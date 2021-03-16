using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsPanelButtons : MonoBehaviour
{
    public Button signOutButton;

    private void Awake()
    {
        FirebaseAuth auth = FirebaseAuth.DefaultInstance;
        signOutButton.onClick.AddListener(() =>
        {
            auth.SignOut();
            SceneLoader.Load(SceneLoader.Scene.Login);
        });

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Tree"))
        {
            Button tree = obj.GetComponent<Button>();
            tree.onClick.AddListener(() =>
            {
                LevelParams.level = tree.transform.Find("Text").GetComponent<Text>().text;
                SceneLoader.Load(SceneLoader.Scene.Game);
            });
        }
    }
}
