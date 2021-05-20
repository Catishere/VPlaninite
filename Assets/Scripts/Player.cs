using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public float movementSpeed;
    Rigidbody2D rb;

    private const float updateSpeed = 5.0f;
    private float AccelerometerUpdateInterval = 1.0f / updateSpeed;
    private float LowPassKernelWidthInSeconds = 1.0f;
    private float LowPassFilterFactor = 0;
    private Vector3 lowPassValue = Vector3.zero;
    private float fullWidth;
    private float halfWidth;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fullWidth = Screen.currentResolution.width * (431f / Screen.dpi);
        halfWidth = Screen.currentResolution.width * (215.5f / Screen.dpi);
        //Filter Accelerometer
        LowPassFilterFactor = AccelerometerUpdateInterval / LowPassKernelWidthInSeconds;
        lowPassValue = Input.acceleration;
    }

    // Update is called once per frame
    void Update()
    {
        LevelParams.Score = Mathf.Max((int)((gameObject.transform.position.y + 100) / 10), LevelParams.Score);
        GameObject.Find("Canvas/Panel/Score").GetComponent<Text>().text = "Toчки: " + LevelParams.Score;
        lowPassValue = Vector3.Lerp(lowPassValue, Input.acceleration, LowPassFilterFactor);

        if (transform.position.x > halfWidth)
            transform.position = new Vector3(transform.position.x - fullWidth, transform.position.y, transform.position.z);
        else if (transform.position.x < -halfWidth)
            transform.position = new Vector3(transform.position.x + fullWidth, transform.position.y, transform.position.z);
    }

    private void FixedUpdate()
    {
        Vector2 velocity = rb.velocity;
        velocity.x = lowPassValue.x * movementSpeed;
        transform.localRotation = velocity.x > 0 ? Quaternion.Euler(0, 180.0f, 0) : Quaternion.Euler(0, -180.0f, 0);
        rb.velocity = velocity;
    }
}
