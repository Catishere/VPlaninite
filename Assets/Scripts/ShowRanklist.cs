using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ShowRanklist : MonoBehaviour
{
    public GameObject list;
    public GameObject listItem; 

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
        valueQuery = _ref.OrderByChild("Highscore").LimitToLast(10);
        valueQuery.ValueChanged += HandleValueChanged;
    }

    private void HandleValueChanged(object sender2, ValueChangedEventArgs e2)
    {
        if (e2.DatabaseError != null)
        {
            Debug.LogError(e2.DatabaseError.Message);
        }

        foreach (Transform child in list.transform)
        {
            Destroy(child.gameObject);
        }

        if (e2.Snapshot != null && e2.Snapshot.ChildrenCount > 0)
        {
            var iter = e2.Snapshot.ChildrenCount;
            foreach (var childSnapshot in e2.Snapshot.Children)
            {
                PlayerData pd = JsonUtility.FromJson<PlayerData>(childSnapshot.GetRawJsonValue());
                if (string.IsNullOrEmpty(pd.Email)) continue;

                var obj = Instantiate(listItem);
                if (pd.Email == LevelParams.Player.Email)
                {
                    obj.transform.Find("Text").GetComponent<Text>().fontStyle = FontStyle.Bold;
                    obj.transform.GetComponent<Image>().color = new Color(1f, 0.73f, 0f, 0.4f);
                }                
                var txtobj = obj.transform.Find("Text").GetComponent<Text>();
                txtobj.text = iter + ". " + pd.Email + ": " + pd.Highscore;
                txtobj.alignment = TextAnchor.MiddleLeft;
                iter--;
                txtobj.fontSize = 35;

                obj.transform.SetParent(list.transform, false);
                obj.transform.SetAsFirstSibling();
            }
        }
    }
    void OnDestroy()
    {
        valueQuery.ValueChanged -= HandleValueChanged;
    }
}
