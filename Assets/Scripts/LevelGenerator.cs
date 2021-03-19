using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject plaformPrefab;

    public int numberOfPlatforms;
    public float levelWidth;
    public float minY;
    public float maxY;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 spawnPosition = new Vector3(0f, -1000f);

        for (int i = 0; i < numberOfPlatforms; i++)
        {
            spawnPosition.y += Random.Range(minY, maxY);
            spawnPosition.x = Random.Range(-levelWidth, levelWidth);
            Instantiate(plaformPrefab, spawnPosition, Quaternion.identity);
        }

        LevelParams.Score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
