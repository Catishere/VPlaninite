﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsPanelButtons : MonoBehaviour
{
    public Button backButton;
    public Sprite[] backgrounds;

    private void Awake()
    {
        transform.Find("MountainLabel").GetComponent<Text>().text = LevelParams.Mountain;
        gameObject.GetComponent<Image>().sprite = LevelParams.Mountain switch
        {
            "Витоша" => backgrounds[0],
            "Рила" => backgrounds[1],
            "Пирин" => backgrounds[2],
            "Стара Планина" => backgrounds[3],
            _ => backgrounds[1]
        };

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
