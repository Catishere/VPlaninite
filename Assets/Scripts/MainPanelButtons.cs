using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanelButtons : MonoBehaviour
{
    public Button signOutButton;
    public Button adventuresButton;
    public Button mapButton;
    public Button rewardsButton;
    public Image profilePicture;

    private void Awake()
    {
        FirebaseAuth auth = FirebaseAuth.DefaultInstance;
        FirebaseUser user = auth.CurrentUser;
        if (user != null)
        {
            string name;
            if (string.IsNullOrEmpty(user.DisplayName))
            {
                name = user.Email.Split('@')[0];
                LevelParams.Player.Email = user.Email;
            }
            else
            {
                name = user.DisplayName.Split(' ')[0];
                LevelParams.Player.Email = user.DisplayName;
            }
            string email = user.Email;

            if (user.PhotoUrl != null)
                StartCoroutine(DownloadTexture.downloadImage(user.PhotoUrl.ToString(), profilePicture));

            transform.Find("ChatBubble/Text").GetComponent<Text>().text = "Здравей, " + name + " готов ли си за приключения?";
            transform.Find("TopPanel/Username").GetComponent<Text>().text = name;
        }

        signOutButton.onClick.AddListener(() =>
        {
            auth.SignOut();
            SceneLoader.Load(SceneLoader.Scene.Login);
        });

        adventuresButton.onClick.AddListener(() =>
        {
            SceneLoader.Load(SceneLoader.Scene.Mountains);
        });

        rewardsButton.onClick.AddListener(() =>
        {
            SceneLoader.Load(SceneLoader.Scene.Rewards);
        });

    }
}
