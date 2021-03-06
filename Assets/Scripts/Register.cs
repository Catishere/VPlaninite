using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Firebase;
using Google;
using Firebase.Auth;
using System;

public class Register : MonoBehaviour
{
    public GameObject email;
    public GameObject password;
    public GameObject popUp;
    public Button registerButton;
    public string webClientId;

    private string pEmail;
    private string pPassword;
    FirebaseAuth auth;
    public PlayerSaveManager playerSaveManager;


    // Start is called before the first frame update
    void Start()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        Button btn = registerButton.GetComponent<Button>();
        btn.onClick.AddListener(register);
    }

    // Update is called once per frame
    void Update()
    {
        pEmail = email.GetComponent<InputField>().text;
        pPassword = password.GetComponent<InputField>().text;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (email.GetComponent<InputField>().isFocused)
            {
                password.GetComponent<InputField>().Select();
            }
            else
            {
                email.GetComponent<InputField>().Select();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (pPassword != "" && pEmail != "")
            {
                register();
            }
        }

        pEmail = email.GetComponent<InputField>().text;
        pPassword = password.GetComponent<InputField>().text;
    }
    void register()
    {
        auth.CreateUserWithEmailAndPasswordAsync(pEmail, pPassword).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                ErrorHandler.displayErrorOnObject(popUp, AuthError.Cancelled);
                Debug.LogError("CANCEL");
            }
            else if (task.IsFaulted)
            {
                FirebaseException fbe = task.Exception.InnerExceptions[0].InnerException as FirebaseException;
                ErrorHandler.displayErrorOnObject(popUp, (AuthError)fbe.ErrorCode);
                Debug.LogError(((AuthError)fbe.ErrorCode).ToString()); 
            }
            else
            {
                FirebaseUser newUser = task.Result;
                Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                    newUser.DisplayName, newUser.UserId);


                PlayerData pd = new PlayerData();
                pd.UserId = newUser.UserId;
                LevelParams.Player = pd;
                try
                {
                    playerSaveManager.SavePlayer(LevelParams.Player);
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }

                UnityMainThread.wkr.AddJob(() =>
                {
                    SceneLoader.Load(SceneLoader.Scene.Login);
                });
            }
        });
    }
}
