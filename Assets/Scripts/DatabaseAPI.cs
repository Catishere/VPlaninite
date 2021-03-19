using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using UnityEngine.UI;

public class DatabaseAPI : MonoBehaviour
{
    private DatabaseReference db;
    void Start()
    {
        db = FirebaseDatabase.DefaultInstance.RootReference;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getUserInfo(string email)
    {
        FirebaseDatabase.DefaultInstance
        .GetReference("Users")
        .OrderByChild("email")
        .EqualTo(email)
        .GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted)
            {
                Debug.Log("Info not found");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                UnityMainThread.wkr.AddJob(() =>
                {
                    transform.Find("Canvas/Panel/Text").GetComponent<Text>().text = snapshot.ToString();
                });
            }
        });
    }

    public void writeNewUser(string userId, User user)
    {
        string json = JsonUtility.ToJson(user);
        db.Child("Users").Child(userId).SetRawJsonValueAsync(json);
    }
    public void WriteNewScore(string userId, int score)
    {
        // Create new entry at /user-scores/$userid/$scoreid and at
        // /leaderboard/$scoreid simultaneously
        string key = db.Child("scores").Push().Key;
        LeaderboardEntry entry = new LeaderboardEntry(userId, score);
        Dictionary<string, object> entryValues = entry.ToDictionary();

        Dictionary<string, object> childUpdates = new Dictionary<string, System.Object>();
        childUpdates["/scores/" + key] = entryValues;
        childUpdates["/user-scores/" + userId + "/" + key] = entryValues;

        db.UpdateChildrenAsync(childUpdates);
    }

    public void AddScoreToLeaders(string email,
                               long score,
                               DatabaseReference leaderBoardRef)
    {

        leaderBoardRef.RunTransaction(mutableData => {
            List<object> leaders = mutableData.Value as List<object>;
    
        if (leaders == null)
            {
                leaders = new List<object>();
            }
            else if (mutableData.ChildrenCount >= 10)
            {
                long minScore = long.MaxValue;
                object minVal = null;
                foreach (var child in leaders)
                {
                    if (!(child is Dictionary<string, object>)) continue;
                    long childScore = (long)
                                ((Dictionary<string, object>)child)["score"];
                    if (childScore < minScore)
                    {
                        minScore = childScore;
                        minVal = child;
                    }
                }
                if (minScore > score)
                {
                    // The new score is lower than the existing 5 scores, abort.
                    return TransactionResult.Abort();
                }

                // Remove the lowest score.
                leaders.Remove(minVal);
            }

            // Add the new high score.
            Dictionary<string, object> newScoreMap =
                             new Dictionary<string, object>();
            newScoreMap["score"] = score;
            newScoreMap["email"] = email;
            leaders.Add(newScoreMap);
            mutableData.Value = leaders;
            return TransactionResult.Success(mutableData);
        });
    }
}
