using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Ground
{
    override public Vector3 Friction(Vector3 currVelocity)
    {
        return new Vector3();
    }
}
