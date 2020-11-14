using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWallController : MonoBehaviour
{
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject waypoint1;
    [SerializeField] private GameObject waypoint2;
    [SerializeField] private float moveSpeed = 2f;

    private Transform targetWaypoint;

    private void Awake()
    {
        targetWaypoint = waypoint1.transform;
    }

    private Vector3 GetVelocity()
    {
        Vector3 proposedVelocityDirection = targetWaypoint.position - wall.transform.position;
        if (proposedVelocityDirection.magnitude < moveSpeed * Time.fixedDeltaTime)
        {
            if (targetWaypoint == waypoint1.transform)
            {
                targetWaypoint = waypoint2.transform;

            } else
            {
                targetWaypoint = waypoint1.transform;
            }
        }
        return proposedVelocityDirection.normalized * moveSpeed;
    }

    private void FixedUpdate()
    {
        wall.GetComponent<Rigidbody>().velocity = GetVelocity();
    }
}
