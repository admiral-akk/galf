using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : Ground
{
    private const float deccelerationMagnitude = -12.5f;

    override public Vector3 Friction(Vector3 currVelocity)
    {
        Debug.Log(currVelocity.magnitude);
        Debug.Log(currVelocity.normalized);
        return deccelerationMagnitude * currVelocity.normalized;
    }
}
