using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            LevelParams.IsWin = true;
            UnityMainThread.wkr.AddJob(() =>
            {
                SceneLoader.Load(SceneLoader.Scene.GameOver);
            });
        }
    }
}
