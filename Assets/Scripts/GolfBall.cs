using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBall : MonoBehaviour
{
    // Physics Properties
    private float maxHitVelocity;
    private float deccelerationMagnitude;

    [Header("Physics Properties")]
    [SerializeField] private float maxDistance = 20f;
    [SerializeField] private float maxTime = 1f;

    [Header("Control Properties")]
    [SerializeField] private float maxPixelDrag = 100f;

    [Header("Game Object Properties")]
    [SerializeField] private GolfBallUI hitUI;

    // Local variables
    Vector3 velocity;
    private Vector3 clickStart;
    private bool moving = false;
    private Vector3 proposedHit;

    private void Awake()
    {
        velocity = new Vector3();

    }

    void FixedUpdate()
    {
        deccelerationMagnitude = -2 * maxDistance / (maxTime * maxTime);
        maxHitVelocity = -deccelerationMagnitude * maxTime;
        hitUI.IsAiming(moving);
        if (velocity.magnitude < 0.001f)
        {
            if (Input.GetMouseButtonDown(0))
            {
                clickStart = Input.mousePosition;
                moving = true;
            }

           if (Input.GetMouseButton(0))
            {
                proposedHit = (Input.mousePosition - clickStart);
                if (proposedHit.magnitude > maxPixelDrag)
                {
                    proposedHit = proposedHit.normalized * maxPixelDrag;
                }

                float hitStrength = proposedHit.magnitude / maxPixelDrag;
                Vector3 hitDirection = new Vector3(proposedHit.normalized.x, proposedHit.normalized.y, 0.0f);
                hitUI.SetIndicator(hitDirection, hitStrength);
            }

            if (Input.GetMouseButtonUp(0) && moving)
            {
                moving = false;

                velocity.x = -maxHitVelocity * proposedHit.x / maxPixelDrag;
                velocity.z = -maxHitVelocity * proposedHit.y / maxPixelDrag;
            }
        }

        Vector3 displacement = velocity * Time.deltaTime;
        transform.localPosition += displacement;

        Vector3 decceleration = velocity.normalized * deccelerationMagnitude * Time.deltaTime ;

        if (decceleration.magnitude > velocity.magnitude)
        {
            velocity = new Vector3();
        } else
        {
            velocity += decceleration;
        }
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
            if (velocity.magnitude < 0.0001)
            {
                Debug.Log("Winner!");
            }
        }
    }
}
