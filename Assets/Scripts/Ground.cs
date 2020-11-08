using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Ground : MonoBehaviour
{
    public const int groundBitMask = 1 << 9;

    abstract public Vector3 Friction(Vector3 currVelocity);
}
