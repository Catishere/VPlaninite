using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneEventListener : MonoBehaviour
{
    public Button SceneChangeButton;
    public SceneLoader.Scene Scene;

    private void Awake()
    {
        SceneChangeButton.onClick.AddListener(() =>
        {
            SceneLoader.Load(Scene);
        });
    }

}
