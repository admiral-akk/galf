using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : Ground
{
    private const float deccelerationMagnitude = -50f;

    override public Vector3 Friction(Vector3 currVelocity)
    {
        return deccelerationMagnitude * currVelocity.normalized;
    }
}
