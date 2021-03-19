public class User
{
    public string username;
    public string email;
    public int HighestScore { get; set; }
    public int GamesPlayed { get; set; }

    public User() {}

    public User(string username, string email)
    {
        this.username = username;
        this.email = email;
    }
}