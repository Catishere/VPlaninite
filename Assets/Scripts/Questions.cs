﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Questions
{
    private static List<Question> _questions = new List<Question>()
    {
        new Question("Колко е висок връх Мусала?", new List<string> {"2925 м.", "2625 м.", "1225 м.", "3225 м."}, "2925 м."),
        new Question("Колко е висок връх Ботев?", new List<string> {"2376 м.", "2267 м.", "2566 м.", "1976 м."}, "2376 м."),
        new Question("Колко е висок връх Вихрен?", new List<string> {"2914 м.", "2415 м.", "2994 м.", "2622 м."}, "2914 м."),
        new Question("Колко е висок Черни връх?", new List<string> {"2198 м.", "2324 м.", "1224 м.", "9924 м."}, "2198 м."),
        new Question("Колко е висок връх Амбарица?", new List<string> {"2166 м.", "2324 м.", "1224 м.", "9924 м."}, "2166 м."),
        new Question("Колко е висок връх Радомир?", new List<string> {"2029 м.", "2324 м.", "1224 м.", "2124 м."}, "2029 м.")
    };

    public static void Shuffle<T>(this IList<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public static Question GetQuestion()
    {
        var question = _questions[Random.Range(0, _questions.Count)];
        question.Answers.Shuffle();
        return question;
    }
}