using System;
public class PlayerData
{
    [NonSerialized]
    public string UserId;
    public string Email;
    public int Highscore;
    public int GamesPlayed;
    public int LevelReached;

    public PlayerData()
    {
    }

    public PlayerData(string userId, string email, int highscore, int gamesPlayed, int levelReached)
    {
        UserId = userId;
        Email = email;
        Highscore = highscore;
        GamesPlayed = gamesPlayed;
        LevelReached = levelReached;
    }
}