using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsPanelButtons : MonoBehaviour
{
    public Button backButton;

    private void Awake()
    {

        transform.Find("MountainLabel").GetComponent<Text>().text = LevelParams.mountain;
        backButton.onClick.AddListener(() =>
        {
            SceneLoader.Load(SceneLoader.Scene.Mountains);
        });

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Tree"))
        {
            Button tree = obj.GetComponent<Button>();
            tree.onClick.AddListener(() =>
            {
                LevelParams.level = tree.transform.Find("Text").GetComponent<Text>().text;
                SceneLoader.Load(SceneLoader.Scene.Game);
            });
        }
    }
}
