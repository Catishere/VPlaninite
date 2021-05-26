using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Firebase;
using Google;
using Firebase.Auth;
using System.Threading.Tasks;

public class Login : MonoBehaviour
{
    public GameObject email;
    public GameObject password;
    public GameObject popUp;
    public PlayerSaveManager playerSaveManager;
    public Button loginButton;
    public Button loginGoogleButton;
    public string webClientId;

    private string pEmail;
    private string pPassword;
    FirebaseAuth auth;
    TaskCompletionSource<FirebaseUser> signInCompleted;


    // Start is called before the first frame update
    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        Button loginButtonComponent = loginButton.GetComponent<Button>();
        Button loginGoogleButtonComponent = loginGoogleButton.GetComponent<Button>();
        loginButtonComponent.onClick.AddListener(login);
        loginGoogleButtonComponent.onClick.AddListener(loginWithGoogle);
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
        loginWithCredentials(credential);
    }

    void loginWithGoogle()
    {
        if (GoogleSignIn.Configuration == null)
        GoogleSignIn.Configuration = new GoogleSignInConfiguration
        {
            RequestIdToken = true,
            WebClientId = webClientId
        };

        Task<GoogleSignInUser> signIn = GoogleSignIn.DefaultInstance.SignIn();

        signInCompleted = new TaskCompletionSource<FirebaseUser>();
        signIn.ContinueWith(task => {
            if (task.IsCanceled)
            {
                signInCompleted.SetCanceled();
                ErrorHandler.displayMessageOnObject(popUp, signIn.Status.ToString());
            }
            else if (task.IsFaulted)
            {
                signInCompleted.SetException(task.Exception);
                GoogleSignIn.SignInException e = task.Exception.InnerExceptions[0] as GoogleSignIn.SignInException;
                ErrorHandler.displayMessageOnObject(popUp, e.Status + e.Message);
            }
            else
            {
                Credential credential = GoogleAuthProvider.GetCredential(task.Result.IdToken, null);
                loginWithCredentials(credential);
            }
        });
    }

    void loginWithCredentials(Credential credential)
    {
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
            else
            {
                FirebaseUser newUser = task.Result;

                if (signInCompleted != null)
                    signInCompleted.SetResult(newUser);

                Debug.LogFormat("User signed in successfully: {0} ({1})",
                    newUser.DisplayName, newUser.UserId);

                if (credential.Provider == "google.com")
                {
                    LevelParams.Player = new PlayerData();
                    LevelParams.Player.UserId = newUser.UserId;
                    UnityMainThread.wkr.AddJob(() =>
                    {
                        SceneLoader.Load(SceneLoader.Scene.Main);
                    });
                }


                playerSaveManager.LoadPlayer(newUser);
            }
        });
    }
}
