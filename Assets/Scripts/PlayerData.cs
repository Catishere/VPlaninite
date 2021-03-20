using System;
public class PlayerData
{
    [NonSerialized]
    public string UserId;
    public string Email;
    public int Highscore;
    public int GamesPlayed;

    public PlayerData()
    {
    }

    public PlayerData(string userId, string email, int highscore, int gamesPlayed)
    {
        UserId = userId;
        Email = email;
        Highscore = highscore;
        GamesPlayed = gamesPlayed;
    }
}