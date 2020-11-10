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

    override public Vector3 Friction(Vector3 currVelocity)
    {
        return moveDirection * moveForce;
    }
}
