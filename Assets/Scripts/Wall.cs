using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    virtual public Vector3 BallVelocity(Vector3 currentVelocity, Collision collision)
    {
        Vector3 aggNormal = new Vector3();
        foreach (ContactPoint contact in collision.contacts)
        {
            aggNormal += contact.normal;
        }
        aggNormal = aggNormal.normalized;
        float similarity = Vector3.Dot(aggNormal, currentVelocity);
        return -2 * similarity * aggNormal + currentVelocity;
    }

}
