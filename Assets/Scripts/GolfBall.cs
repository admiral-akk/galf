using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBall : MonoBehaviour
{
    // Physics Properties
    private float hitVelocity;
    private float deccelerationMagnitude;

    [Header("Physics Properties")]
    [SerializeField] private float maxDistance = 20f;
    [SerializeField] private float maxTime = 1f;

    [Header("Control Properties")]
    [SerializeField] private float maxPixelDrag = 100f;

    [Header("Game Object Properties")]
    [SerializeField] private GolfBallUI hitUI;

    // Local variables
    private Vector3 velocity;
    private Vector3 clickStart;
    private Vector3 proposedHit;
    private InteractionState state;

    enum InteractionState
    {
        Ready,
        Aiming,
        Moving,
        Paused,
    }

    private void Awake()
    {
        velocity = new Vector3();
        deccelerationMagnitude = -2 * maxDistance / (maxTime * maxTime);
        state = InteractionState.Ready;
    }

    private Vector3 CalculateProposedHit(Vector3 mouseStart, Vector3 mouseCurr)
    {
        // Rotate due to camera angle
        Vector3 projectedMouseDrag = Quaternion.AngleAxis(-45, Vector3.forward) * (mouseCurr - mouseStart);

        // Cap the amount that the hit can be dragged
        if (projectedMouseDrag.magnitude > maxPixelDrag)
        {
            projectedMouseDrag = projectedMouseDrag.normalized * maxPixelDrag;
        }

        float hitStrength = projectedMouseDrag.magnitude / maxPixelDrag;

        // Velocity doesn't scale linearly with hit strength since we want the
        // travel distance to scale lineraly with hit strength.
        hitVelocity = Mathf.Sqrt(-2 * maxDistance * hitStrength * deccelerationMagnitude);
        Vector3 proposedVelocity = projectedMouseDrag.normalized * hitVelocity;
        return proposedVelocity;
    }

    private void UpdateHitUI(Vector3 proposedHit)
    {
        hitUI.IsAiming(state == InteractionState.Aiming);
        if (state != InteractionState.Aiming)
        {
            return;
        }
        Vector3 hitDirection = new Vector3(proposedHit.normalized.x, proposedHit.normalized.y, 0.0f);
        float hitStrength = -proposedHit.magnitude * proposedHit.magnitude / (2 * maxDistance * deccelerationMagnitude);
        hitUI.SetIndicator(hitDirection, hitStrength);
    }

    private void HandleInput()
    {
        if (state != InteractionState.Ready && state != InteractionState.Aiming)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && state == InteractionState.Ready)
        {
            clickStart = Input.mousePosition;
            state = InteractionState.Aiming;
        }

        if (state != InteractionState.Aiming)
        {
            return;
        }

        if (Input.GetMouseButton(0))
        {
            proposedHit = CalculateProposedHit(clickStart, Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            state = InteractionState.Moving;
            velocity.x = -proposedHit.x;
            velocity.z = -proposedHit.y;
        }
    }

    private void ApplyMotion()
    {
        Vector3 displacement = velocity * Time.deltaTime;
        transform.localPosition += displacement;
    }

    private void ApplyDeceleration()
    {
        if (state != InteractionState.Moving)
        {
            return;
        }
        float velocityLoss = deccelerationMagnitude * Time.deltaTime;
        if (-velocityLoss > velocity.magnitude)
        {
            velocity = new Vector3();
            state = InteractionState.Ready;
        }
        else
        {
            velocity += velocity.normalized * velocityLoss;
        }
    }

    void FixedUpdate()
    {
        HandleInput();
        UpdateHitUI(proposedHit);

        ApplyMotion();
        ApplyDeceleration();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Vector3 aggNormal = new Vector3();
            foreach (ContactPoint contact in collision.contacts)
            {
                aggNormal += contact.normal;
            }
            aggNormal = aggNormal.normalized;
            float similarity = Vector3.Dot(aggNormal, velocity);
            velocity += -2 * similarity * aggNormal;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Goal"))
        {
            if (state == InteractionState.Ready)
            {
                state = InteractionState.Paused;
                LevelManager.instance.TriggerWinScreen();
            }
        }
    }
}
