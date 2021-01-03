using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt: Ground
{
    private Vector3 moveDirection = new Vector3(0, 0, 1);
    [SerializeField] private float moveForce = 10f;


    float rotation = 80;
    float scale = 10;
    Vector3 offset = new Vector3(0.5f, 0.5f, 0);
    Vector3 tiling = new Vector3(1, 1, 1);

    private void Awake()
    {
        moveDirection.y = 0;
        moveDirection = moveDirection.normalized;
        moveDirection = Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) * moveDirection;

        GetComponent<MeshRenderer>().material.SetTextureScale("_MainTex", new Vector2(transform.localScale.x / 2, transform.localScale.z / 2));
        GetComponent<MeshRenderer>().material.SetTextureOffset("_MainTex", new Vector2(0.2f, 0f));
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
