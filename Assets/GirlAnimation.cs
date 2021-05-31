using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GirlAnimation : MonoBehaviour
{
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
    private bool interrupt;
    private bool end;

    public PlayerSaveManager playerSaveManager;
    void Start()
    {
        girlStartPos = girl.transform.localPosition;
        messageStartPos = message.transform.localPosition;

        girlPos = new Vector2(girlStartPos.x - 500f, girl.transform.localPosition.y);
        messagePos = new Vector2(messageStartPos.x + 800f, message.transform.localPosition.y);
        message.transform.GetComponent<Button>().onClick.AddListener(() =>
        {
            interrupt = true;
        });

        if (LevelParams.Player.QuizScoreKeys.Contains(LevelParams.Mountain))
        {
            var index = LevelParams.Player.QuizScoreKeys.IndexOf(LevelParams.Mountain);
            LevelParams.Player.QuizScoreValues[index] = Math.Max(LevelParams.QuizResult, LevelParams.Player.QuizScoreValues[index]);
        }
        else
        {
            LevelParams.Player.QuizScoreKeys.Add(LevelParams.Mountain);
            LevelParams.Player.QuizScoreValues.Add(LevelParams.QuizResult);
        }


        playerSaveManager.SavePlayer(LevelParams.Player);

        message.transform.Find("Text").GetComponent<Text>().text = "Браво! ти изкара " + LevelParams.QuizResult + " от 6 точки на теста!";

        translateGirl = true;
        StartCoroutine(Coroutine());
    }

    IEnumerator Coroutine()
    {
        yield return Wait(10);
        translateGirl = true;
        girlPos = girlStartPos;
        messagePos = messageStartPos;
        end = true;
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

    // Update is called once per frame
    void Update()
    {
        if (Math.Abs(girl.transform.localPosition.x - girlPos.x) < 0.1f)
        {
            translateGirl = false;
            if (end)
                SceneLoader.Load(SceneLoader.Scene.Levels);
        }

        if (!translateGirl) return;

        girl.transform.localPosition =
            Vector3.SmoothDamp(girl.transform.localPosition, girlPos, ref velocity, smoothTime);
        message.transform.localPosition =
            Vector3.SmoothDamp(message.transform.localPosition, messagePos, ref velocity1, smoothTime);
    }
}
