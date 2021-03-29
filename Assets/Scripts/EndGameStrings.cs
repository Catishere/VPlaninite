using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EndGameStrings
{
    private static List<string> EndGameSrings = new List<string>()
    {
        "Подхлъзна се на мокрите камъни и падна от скалата.",
        "Затрупа те лавина.",
        "Застигна те участта на Христо Проданов.",
        "Ухапа те отровна змия.",
        "Изяде те мечка.",
        "Просто спри играта."
    };

    private static List<string> WinEndGameSrings = new List<string>()
    {
        "Алпиниста на квартала",
        "Планинска коза",
        "Май си нямаме работа, а?"
    };

    public static string GetString()
    {
        if (LevelParams.IsWin)
            return WinEndGameSrings[Random.Range(0, WinEndGameSrings.Count)];
        return EndGameSrings[Random.Range(0, EndGameSrings.Count)];
    }
}
