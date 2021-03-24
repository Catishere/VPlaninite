using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public float movementSpeed;
    public Camera mainCamera;
    Rigidbody2D rb;

    private const float updateSpeed = 5.0f;
    private float AccelerometerUpdateInterval = 1.0f / updateSpeed;
    private float LowPassKernelWidthInSeconds = 1.0f;
    private float LowPassFilterFactor = 0;
    private Vector3 lowPassValue = Vector3.zero;
    private float fullScreen;
    private float halfScreen;

    void Start()
    {
        fullScreen = mainCamera.pixelWidth;
        halfScreen = mainCamera.pixelWidth / 2;
        rb = GetComponent<Rigidbody2D>();    
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

        if (transform.position.x > halfScreen)
            transform.position = new Vector3(transform.position.x - fullScreen, transform.position.y, transform.position.z);
        else if (transform.position.x < -halfScreen)
            transform.position = new Vector3(transform.position.x + fullScreen, transform.position.y, transform.position.z);
    }

    private void FixedUpdate()
    {
        Vector2 velocity = rb.velocity;
        velocity.x = lowPassValue.x * movementSpeed;
        rb.velocity = velocity;
    }
}
