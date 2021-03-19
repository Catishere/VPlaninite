using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverInit : MonoBehaviour
{
    public Button retryButton;
    public Button levelsButton;
    public Text gameOverText;
    // Start is called before the first frame update
    private void Awake()
    {
        gameOverText.text = EndGameStrings.GetString();

        retryButton.onClick.AddListener(() =>
        {
            SceneLoader.Load(SceneLoader.Scene.Game);
        });

        levelsButton.onClick.AddListener(() =>
        {
            SceneLoader.Load(SceneLoader.Scene.Levels);
        });
    }
}
