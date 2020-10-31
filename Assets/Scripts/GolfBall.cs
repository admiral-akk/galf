using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBall : MonoBehaviour
{
    Vector3 velocity;
    [SerializeField] private float acceleration = 10f;


    void FixedUpdate()
    {
        Vector2 playerInput = Vector2.ClampMagnitude(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")), 1f);
        velocity.x += playerInput.x * Time.deltaTime * acceleration;
        velocity.z += playerInput.y * Time.deltaTime * acceleration;
        Vector3 displacement = velocity * Time.deltaTime;
        transform.localPosition += displacement;
    }
}
