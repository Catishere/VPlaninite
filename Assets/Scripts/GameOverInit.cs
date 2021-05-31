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
    private IEnumerator coroutine;
    private bool interrupt;

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
                if (LevelParams.Level == "6")
                    GameObject.Find("Panel/RetryButton/Text").GetComponent<Text>().text = "Към теста";

                girlPos = new Vector2(girlStartPos.x - 500f, girl.transform.localPosition.y);
                messagePos = new Vector2(messageStartPos.x + 800f, message.transform.localPosition.y);
                translateGirl = true;
                var mountainMessages = LevelGameOverMessages.messages[LevelParams.Mountain];
                var levelMessages = mountainMessages[int.Parse(LevelParams.Level) - 1];
                coroutine = MessageEndCoroutine(levelMessages);
                StartCoroutine(coroutine);

                if (LevelParams.Player.LevelReachedKeys.Contains(LevelParams.Mountain))
                {
                    var index = LevelParams.Player.LevelReachedKeys.IndexOf(LevelParams.Mountain);
                    LevelParams.Player.LevelReachedValues[index] = Math.Max(int.Parse(LevelParams.Level) + 1, LevelParams.Player.LevelReachedValues[index]);
                }
                else
                {
                    LevelParams.Player.LevelReachedKeys.Add(LevelParams.Mountain);
                    LevelParams.Player.LevelReachedValues.Add(int.Parse(LevelParams.Level) + 1);
                }
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
            SceneLoader.Load(LevelParams.IsWin && LevelParams.Level == "6" ? SceneLoader.Scene.Map : SceneLoader.Scene.Game);
            LevelParams.IsWin = false;
        });

        levelsButton.onClick.AddListener(() =>
        {
            LevelParams.IsWin = false;
            SceneLoader.Load(SceneLoader.Scene.Levels);
        });

        message.transform.GetComponent<Button>().onClick.AddListener(() =>
        {
            interrupt = true;
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

    IEnumerator Wait(float waitTime)
    {
        for (float timer = waitTime; timer >= 0; timer -= Time.deltaTime)
        {
            if (interrupt)
            {
                interrupt = false;
                yield break;
            }
            yield return null;
        }
    }

    IEnumerator MessageEndCoroutine(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            var j = i;
            UnityMainThread.wkr.AddJob(() =>
            {
                message.transform.Find("Text").GetComponent<Text>().text = list[j];
            });
            yield return Wait(6);
        }

        translateGirl = true;
        girlPos = girlStartPos;
        messagePos = messageStartPos;
    }
}
