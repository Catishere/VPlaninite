using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveManager : MonoBehaviour
{
    private const string user_key = "users";
    private FirebaseDatabase _database;
    private DatabaseReference _ref;

    public PlayerData LastPlayerData { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        _database = FirebaseDatabase.DefaultInstance;
        _ref = _database.GetReference(user_key);
    }

    public void SavePlayer(PlayerData player)
    {
        if (!player.Equals(LastPlayerData))
        {
            _ref.Child(player.UserId).SetRawJsonValueAsync(JsonUtility.ToJson(player)).ContinueWith((action) =>
            {
                if (action.IsFaulted)
                {
                    Debug.LogError(action.Exception);
                }
                else
                    Debug.Log(action.Status);
            });
        }
    }

    public void LoadPlayer(string userId)
    {
        _ref.Child(userId).GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted)
            {
                Debug.LogError(task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                PlayerData playerData = new PlayerData();
                playerData.UserId = userId;

                try
                {
                    string a = snapshot.Child(userId).Child("GamesPlayed").Value.ToString();
                    string b = snapshot.Child(userId).Child("Highscore").Value.ToString();
                    Debug.Log(a + b);
                    playerData.GamesPlayed = 0;
                    playerData.Highscore = 0;
                } catch (Exception e)
                {
                    Debug.Log(e);
                }

                Debug.Log("Loaded Player" + JsonUtility.ToJson(playerData));
                LevelParams.Player = playerData;
            }
        });
    }
}
