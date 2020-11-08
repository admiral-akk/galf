using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : Ground
{
    override public Vector3 Friction(Vector3 currVelocity)
    {
        return new Vector3();
    }
}
