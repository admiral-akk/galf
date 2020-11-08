using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBall : MonoBehaviour
{
    [Header("Physics Properties")]
    [SerializeField] private float maxVelocity = 25f;

    [Header("Control Properties")]
    [SerializeField] private float maxPixelDrag = 100f;

    [Header("Game Object Properties")]
    [SerializeField] private GolfBallUI hitUI;

    // Local variables
    private Vector3 velocity;
    private Vector3 clickStart;
    private Vector3 proposedHit;
    private InteractionState state;

    public static GolfBall instance;

    enum InteractionState
    {
        Ready,
        Aiming,
        Moving,
        Paused,
    }

    private void Awake()
    {
        instance = this;
        velocity = new Vector3();
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

        Vector3 proposedVelocity = projectedMouseDrag.normalized * hitStrength * maxVelocity;
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
        float hitStrength =  proposedHit.magnitude / maxVelocity;
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

    private Ground GetGround()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, Ground.groundBitMask))
        {
            return hit.collider.gameObject.GetComponent<Ground>();
        }
        return null;
    }

    private void ApplyDeceleration()
    {
        if (state != InteractionState.Moving)
        {
            return;
        }

        Ground ground = GetGround();

        if (ground == null)
        {
            Debug.LogError("No ground!");
            return;
        }

        Vector3 frictionVelocityDelta = ground.Friction(velocity) * Time.deltaTime;
        if (velocity == Vector3.zero || frictionVelocityDelta.magnitude > velocity.magnitude)
        {
            velocity = new Vector3();
            state = InteractionState.Ready;
        }
        else
        {
            velocity += frictionVelocityDelta;
            }

        GetComponent<Rigidbody>().velocity = velocity;
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
            velocity = -2 * similarity * aggNormal + velocity;
            GetComponent<Rigidbody>().velocity = velocity;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Goal"))
        {
            if (state == InteractionState.Ready)
            {
                state = InteractionState.Paused;
                GameManager.instance.Win();
            }
        }
    }
}
