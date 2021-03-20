using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverInit : MonoBehaviour
{
    public Button retryButton;
    public Button levelsButton;
    public Text gameOverText;
    public PlayerSaveManager playerSaveManager;

    // Start is called before the first frame update
    private void Start()
    {
        gameOverText.text = EndGameStrings.GetString();

        GameObject.Find("Panel/Score").GetComponent<Text>().text = "Резултат\n" + LevelParams.Score;

        if (LevelParams.Player != null)
        {
            LevelParams.Player.GamesPlayed++;
            if (LevelParams.Player.Highscore < LevelParams.Score)
            {
                LevelParams.Player.Highscore = LevelParams.Score;
            }
            playerSaveManager.SavePlayer(LevelParams.Player);
        }

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
