using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Ground : MonoBehaviour
{
    public const int groundBitMask = 1 << 9;


    virtual public bool HasEffect()
    {
        return false;
    }

    virtual public void Effect(GolfBall ball)
    {
    }

    virtual public bool HasFriction() {
        return true;
    }

    virtual public Vector3 Friction(Vector3 currVelocity) {
        return new Vector3();
    }

    virtual public bool HasForce()
    {
        return false;
    }

    virtual public Vector3 Force()
    {
        return new Vector3();
    }

}
