using Firebase.Auth;
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
                Debug.Log(JsonUtility.ToJson(player));
                if (action.IsFaulted)
                {
                    Debug.LogError("Save Player" + action.Exception);
                }
                else
                    Debug.Log("Save Player" + action.ToString());
            });
        }
    }

    public void LoadPlayer(FirebaseUser user)
    {
        _ref.Child(user.UserId).GetValueAsync().ContinueWith(task => {
            
            PlayerData playerData;

            if (task.IsFaulted)
            {
                Debug.LogError(task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                playerData = JsonUtility.FromJson<PlayerData>(snapshot.GetRawJsonValue());
                playerData.UserId = user.UserId;
                Debug.Log("STARTL::LL:L" + user.Email + "  " + user.DisplayName);
                playerData.Email = string.IsNullOrEmpty(user.Email) ? user.DisplayName : user.Email;
                LevelParams.Player = playerData;
            }

            UnityMainThread.wkr.AddJob(() =>
            {
                SceneLoader.Load(SceneLoader.Scene.Main);
            });
        });
    }
}
