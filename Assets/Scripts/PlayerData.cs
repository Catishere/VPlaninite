using System;
public class PlayerData
{
    [NonSerialized]
    public string UserId;
    public int Highscore;
    public int GamesPlayed;

    public PlayerData()
    {
    }

    public PlayerData(string userId, int highscore, int gamesPlayed)
    {
        UserId = userId;
        Highscore = highscore;
        GamesPlayed = gamesPlayed;
    }
}