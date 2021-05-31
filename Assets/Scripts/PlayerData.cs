using System;
using System.Collections.Generic;

public class PlayerData
{
    [NonSerialized]
    public string UserId;
    public string Email;
    public int Highscore;
    public int GamesPlayed;
    public List<string> LevelReachedKeys;
    public List<int> LevelReachedValues;
    public List<string> QuizScoreKeys;
    public List<int> QuizScoreValues;

    public PlayerData()
    {
        LevelReachedKeys = new List<string>();
        LevelReachedValues = new List<int>();
        QuizScoreKeys = new List<string>();
        QuizScoreValues = new List<int>();
    }

    public PlayerData(string userId, string email, int highscore, int gamesPlayed)
    {
        UserId = userId;
        Email = email;
        Highscore = highscore;
        GamesPlayed = gamesPlayed;
        LevelReachedKeys = new List<string>();
        LevelReachedValues = new List<int>();
        QuizScoreKeys = new List<string>();
        QuizScoreValues = new List<int>();
    }
}