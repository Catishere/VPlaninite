﻿using Firebase.Auth;
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
            if (user.DisplayName.Equals(string.Empty))
                name = user.Email.Split('@')[0];
            else
                name = user.DisplayName.Split(' ')[0];

            string email = user.Email;

            if (user.PhotoUrl != null)
                StartCoroutine(DownloadTexture.downloadImage(user.PhotoUrl.ToString(), profilePicture));

            transform.Find("ChatBubble/Text").GetComponent<Text>().text = "Здр, " + name + " кп?";
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

        mapButton.onClick.AddListener(() =>
        {
            SceneLoader.Load(SceneLoader.Scene.Map);
        });

        rewardsButton.onClick.AddListener(() =>
        {
            SceneLoader.Load(SceneLoader.Scene.Rewards);
        });

    }
}