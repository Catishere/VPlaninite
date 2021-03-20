using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardsButtons : MonoBehaviour
{
    public Button backButton;
    public ShowRanklist showRanklist;
    // Start is called before the first frame update
    private void Awake()
    {
        showRanklist.LoadRanklist();
        backButton.onClick.AddListener(() =>
        {
            SceneLoader.Load(SceneLoader.Scene.Main);
        });
    }
}
