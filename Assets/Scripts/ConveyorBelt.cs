using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt: Ground
{
    [SerializeField] private Vector3 moveDirection = new Vector3(1, 0, 1);
    [SerializeField] private float moveForce = 10f;

    private void Awake()
    {
        moveDirection.y = 0;
        moveDirection = moveDirection.normalized;
    }

    public override bool HasFriction()
    {
        return false;
    }

    public override bool HasForce()
    {
        return true;
    }

    public override Vector3 Force()
    {
        return moveDirection * moveForce;
    }
}
