using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question
{
    public string QuestionString { get; set; }
    public List<string> Answers { get; set; }
    public string Correct { get; set; }

    public Question()
    {
    }

    public Question(string questionString, List<string> answers, string correct)
    {
        QuestionString = questionString;
        Answers = answers;
        Correct = correct;
    }
}
