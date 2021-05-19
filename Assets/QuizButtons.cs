using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class QuizButtons : MonoBehaviour
{
    public Text Question;
    private Question _question;
    public Button[] Buttons;
    private readonly Text[] _buttonTexts = new Text[10];
    
    private void Awake()
    {
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
            });
        }

    }

    IEnumerator QuestionCoroutine()
    {
        yield return new WaitForSeconds(3);
        SetUpQuestion();
    }

    private void SetUpQuestion()
    {
        _question = Questions.GetQuestion();

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
