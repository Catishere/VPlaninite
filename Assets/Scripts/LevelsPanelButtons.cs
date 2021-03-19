using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsPanelButtons : MonoBehaviour
{
    public Button backButton;

    private void Awake()
    {

        transform.Find("MountainLabel").GetComponent<Text>().text = LevelParams.Mountain;
        backButton.onClick.AddListener(() =>
        {
            SceneLoader.Load(SceneLoader.Scene.Mountains);
        });

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Tree"))
        {
            Button tree = obj.GetComponent<Button>();
            tree.onClick.AddListener(() =>
            {
                LevelParams.Level = tree.transform.Find("Text").GetComponent<Text>().text;
                SceneLoader.Load(SceneLoader.Scene.Game);
            });
        }
    }
}
