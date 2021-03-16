using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnLevelOpen : MonoBehaviour
{
    void Start()
    {
        transform.Find("LevelLabel").GetComponent<Text>().text = "Level " + LevelParams.level;
        Button returnButton = transform.Find("ReturnButton").GetComponent<Button>();
        returnButton.onClick.AddListener(() =>
        {
            SceneLoader.Load(SceneLoader.Scene.Levels);
        });
    }
    void Update()
    {
        
    }
}
