using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Firebase;
using Google;
using Firebase.Auth;

public class Login : MonoBehaviour
{
    public GameObject email;
    public GameObject password;
    public GameObject popUp;
    public Button loginButton;
    public string webClientId;

    private string pEmail;
    private string pPassword;
    FirebaseAuth auth;


    // Start is called before the first frame update
    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        Button btn = loginButton.GetComponent<Button>();
        btn.onClick.AddListener(login);
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

        if (Input.GetKeyDown(KeyCode.Return)) {
            if (pPassword != "" && pEmail != "") {
                login();
            }
        }

        pEmail = email.GetComponent<InputField>().text;
        pPassword = password.GetComponent<InputField>().text;
    }
    void login()
    {
        Credential credential =
        EmailAuthProvider.GetCredential(pEmail, pPassword);

        auth.SignInWithCredentialAsync(credential).ContinueWith(task => {
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
            else {
                FirebaseUser newUser = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                    newUser.DisplayName, newUser.UserId);

                UnityMainThread.wkr.AddJob(() =>
                {
                    SceneLoader.Load(SceneLoader.Scene.Levels);
                });
            }
        });
    }
}
