using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBall : MonoBehaviour
{
    // Physics Properties
    [SerializeField] private float acceleration = 20f;
    [SerializeField] private float deccelerationMagnitude = 0.1f;

    [SerializeField] private float maxDistance = 20f;
    [SerializeField] private float maxTime = 1f;

    // Control Properties
    [SerializeField] private float maxPixelDrag = 100f;

    // Game Object Properties
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

                velocity.x = - acceleration * proposedHit.x / maxPixelDrag;
                velocity.z = - acceleration * proposedHit.y / maxPixelDrag;
            }
        }

        Vector3 displacement = velocity * Time.deltaTime;
        transform.localPosition += displacement;

        Vector3 decceleration = -velocity.normalized * deccelerationMagnitude;

        if (decceleration.magnitude > velocity.magnitude)
        {
            velocity = new Vector3();
        } else
        {
            velocity += decceleration;
        }
    }
}
