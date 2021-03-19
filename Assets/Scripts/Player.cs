using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public float movement = 0f;
    public float movementSpeed = 1500f;
    Rigidbody2D rb;
    Gyroscope m_Gyro;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        m_Gyro = Input.gyro;
        m_Gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        movement = m_Gyro.rotationRateUnbiased.y * movementSpeed;
    }
    private void FixedUpdate()
    {
        Vector2 velocity = rb.velocity;
        velocity.x = movement;
        rb.velocity = velocity;
    }
}
