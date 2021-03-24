using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] plaformPrefabs;
    public int numberOfPlatforms;
    public float levelWidth;
    public float minY;
    public float maxY;

    private bool isAwake = false;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 spawnPosition = new Vector3(0f, -1000f);
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;

        for (int i = 0; i < numberOfPlatforms; i++)
        {
            spawnPosition.y += Random.Range(minY, maxY);
            spawnPosition.x = Random.Range(-levelWidth, levelWidth);
            int rng = Random.Range(0, 100);
            int plat;

            if (rng > 95)
                plat = 1;
            else if (rng > 80)
                plat = 2;
            else
                plat = 0;

            Instantiate(plaformPrefabs[plat], spawnPosition, Quaternion.identity);
        }

        LevelParams.Score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAwake && Input.touchCount > 0)
        {
            GameObject.Find("Player").GetComponent<Rigidbody2D>().WakeUp();
            isAwake = true;
        }
    }
}
