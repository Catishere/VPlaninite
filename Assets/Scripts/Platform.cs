using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Platform : MonoBehaviour
{
    public float jumpForce;
    public Text score;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();

        if (collision.relativeVelocity.y <= 0f)
        {
            if (rb != null)
            {
                LevelParams.Score++;
                score.text = "Toчки: " + LevelParams.Score;

                Vector2 velocity = rb.velocity;
                velocity.y = jumpForce;
                rb.velocity = velocity;
            }
        }
    }
}
