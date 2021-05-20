using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class QuizButtons : MonoBehaviour
{
    public Text Question;
    private Question _question;
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
    public Button[] Buttons;
    private readonly Text[] _buttonTexts = new Text[10];

    private void Awake()
    {
        girlStartPos = girl.transform.localPosition;
        messageStartPos = message.transform.localPosition;
        SetUpQuestion();
        var colors = Buttons[0].colors;
        for (int i = 0; i < Buttons.Length; i++)
        {
            var k = i;
            Buttons[i].onClick.AddListener(() =>
            {
                if (Buttons[k].colors.selectedColor == Color.white && Buttons[k].enabled)
                    StartCoroutine(QuestionCoroutine());

                colors.selectedColor = _buttonTexts[k].text == _question.Correct ? Color.green : Color.red;
                colors.pressedColor = colors.selectedColor;
                colors.normalColor = colors.selectedColor;
                Buttons[k].colors = colors;
                foreach (var button in Buttons)
                    if (!button.Equals(Buttons[k]))
                        button.enabled = false;
                if (colors.selectedColor == Color.green)
                {
                    girlPos = new Vector2(girlStartPos.x - 670f, girl.transform.localPosition.y);
                    messagePos = new Vector2(messageStartPos.x + 820f, message.transform.localPosition.y);
                    message.transform.Find("Text").GetComponent<Text>().text = GetCongratulationString();
                    translateGirl = true;
                }
            });
        }

    }

    private string GetCongratulationString()
    {
        var congrats = new List<string>
        {
            "Браво!",
            "Поздравления!",
            "Отлично!"
        };
        return congrats[UnityEngine.Random.Range(0, congrats.Count)];
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

    IEnumerator QuestionCoroutine()
    {
        yield return new WaitForSeconds(3);
        translateGirl = true;
        girlPos = girlStartPos;
        messagePos = messageStartPos;
        SetUpQuestion();
    }

    private void SetUpQuestion()
    {
        Question q;
        do
        {
           q = Questions.GetQuestion();
        } while (q.QuestionString == _question.QuestionString);

        _question = q;

        UnityMainThread.wkr.AddJob(() =>
        {
            foreach (var button in Buttons)
                button.enabled = true;

            Question.text = _question.QuestionString;
            for (int i = 0; i < Buttons.Length; i++)
            {
                _buttonTexts[i] = Buttons[i].transform.GetChild(0).GetComponent<Text>();
                _buttonTexts[i].text = _question.Answers[i];
            }

            foreach (var button in Buttons)
            {
                var colors = button.colors;
                colors.selectedColor = Color.white;
                colors.pressedColor = Color.white;
                colors.normalColor = Color.white;
                button.colors = colors;
            }
        });
    }
} 
