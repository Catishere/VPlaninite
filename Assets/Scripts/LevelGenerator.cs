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

        float step = 0;
        if (LevelParams.Level != null)
            step = 50 * int.Parse(LevelParams.Level);

        for (int i = 0; i < numberOfPlatforms; i++)
        {
            spawnPosition.y += Random.Range(minY + step, maxY);
            spawnPosition.x = Random.Range(-levelWidth, levelWidth);
            Instantiate(plaformPrefabs[0], spawnPosition, Quaternion.identity);
        }

        spawnPosition.x = 0;

        Instantiate(plaformPrefabs[3], spawnPosition, Quaternion.identity);

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
