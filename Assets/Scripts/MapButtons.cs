using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapButtons : MonoBehaviour
{
    public Button backButton;
    // Start is called before the first frame update
    private void Awake()
    {
        backButton.onClick.AddListener(() =>
        {
            SceneLoader.Load(SceneLoader.Scene.Main);
        });
    }
}
