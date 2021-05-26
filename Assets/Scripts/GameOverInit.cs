using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverInit : MonoBehaviour
{
    public Button retryButton;
    public Button levelsButton;
    public Text gameOverText;

    public GameObject girl;
    public GameObject message;
    private Vector3 velocity;
    private float smoothTime = 0.5f;
    private Vector3 velocity1;
    private Vector2 girlStartPos;
    private Vector2 messageStartPos;
    private Vector2 girlPos;
    private Vector2 messagePos;
    private bool translateGirl;

    public PlayerSaveManager playerSaveManager;

    // Start is called before the first frame update
    private void Start()
    {
        girlStartPos = girl.transform.localPosition;
        messageStartPos = message.transform.localPosition;
        gameOverText.text = EndGameStrings.GetString();

        GameObject.Find("Panel/Score").GetComponent<Text>().text = "Резултат\n" + LevelParams.Score;

        if (LevelParams.Player != null)
        {
            if (LevelParams.IsWin)
            {
                girlPos = new Vector2(girlStartPos.x - 500f, girl.transform.localPosition.y);
                messagePos = new Vector2(messageStartPos.x + 800f, message.transform.localPosition.y);
                message.transform.Find("Text").GetComponent<Text>().text = "Браво";
                translateGirl = true;
                StartCoroutine(MessageEndCoroutine());

                if (LevelParams.Player.LevelReachedKeys.Contains(LevelParams.Mountain))
                    LevelParams.Player.LevelReachedValues[LevelParams.Player.LevelReachedKeys.IndexOf(LevelParams.Mountain)] = int.Parse(LevelParams.Level) + 1;
                else
                {
                    LevelParams.Player.LevelReachedKeys.Add(LevelParams.Mountain);
                    LevelParams.Player.LevelReachedValues.Add(int.Parse(LevelParams.Level) + 1);
                }

                LevelParams.IsWin = false;
            }

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
    private void Update()
    {
        if (Math.Abs(girl.transform.localPosition.x - girlPos.x) < 0.1f)
            translateGirl = false;

        if (!translateGirl) return;

        girl.transform.localPosition =
            Vector3.SmoothDamp(girl.transform.localPosition, girlPos, ref velocity, smoothTime);
        message.transform.localPosition =
            Vector3.SmoothDamp(message.transform.localPosition, messagePos, ref velocity1, smoothTime);
    }

    IEnumerator MessageEndCoroutine()
    {
        yield return new WaitForSeconds(3);
        translateGirl = true;
        girlPos = girlStartPos;
        messagePos = messageStartPos;
    }
}
