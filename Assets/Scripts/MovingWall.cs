using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : Wall
{
    override public Vector3 BallVelocity(Vector3 currentVelocity, Collision collision)
    {
        Vector3 wallVelocity = GetComponent<Rigidbody>().velocity;
        return base.BallVelocity(currentVelocity - wallVelocity, collision) + wallVelocity;
    }

}
