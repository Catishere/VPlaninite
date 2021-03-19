using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardsButtons : MonoBehaviour
{
    public Text text;
    public Button backButton;
    public DatabaseAPI db;
    // Start is called before the first frame update
    private void Awake()
    {
        db.getUserInfo(LevelParams.User.email);
        backButton.onClick.AddListener(() =>
        {
            SceneLoader.Load(SceneLoader.Scene.Main);
        });
    }
}
