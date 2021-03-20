using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ShowRanklist : MonoBehaviour
{
    public Text ranking;
    private const string user_key = "users";
    private FirebaseDatabase _database;
    private DatabaseReference _ref;
    private Query valueQuery;

    public PlayerData LastPlayerData { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        _database = FirebaseDatabase.DefaultInstance;
        _ref = _database.GetReference(user_key);
    }

    public void LoadRanklist()
    {
        valueQuery = _ref.OrderByChild("Highscore").OrderByValue().LimitToLast(5);
        valueQuery.ValueChanged += HandleValueChanged;
    }

    private void HandleValueChanged(object sender2, ValueChangedEventArgs e2)
    {
        if (e2.DatabaseError != null)
        {
            Debug.LogError(e2.DatabaseError.Message);
        }

        if (e2.Snapshot != null && e2.Snapshot.ChildrenCount > 0)
        {
            ranking.text = "";
            foreach (var childSnapshot in e2.Snapshot.Children)
            {
                PlayerData pd = JsonUtility.FromJson<PlayerData>(childSnapshot.GetRawJsonValue());
                ranking.text += pd.Email + ": " + pd.Highscore + "\n";
            }
        }
    }
    void OnDestroy()
    {
        valueQuery.ValueChanged -= HandleValueChanged;
    }
}
