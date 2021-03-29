using Firebase;
using Firebase.Auth;
using Google;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GoogleLoginOnLoad : MonoBehaviour
{
    public PlayerSaveManager playerSaveManager;
    public string webClientId;

    private string pEmail;
    private string pPassword;
    FirebaseAuth auth;
    TaskCompletionSource<FirebaseUser> signInCompleted;


    // Start is called before the first frame update
    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        loginWithGoogle();
    }

    // Update is called once per frame
    void Update()
    {
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
                UnityMainThread.wkr.AddJob(() =>
                {
                    SceneLoader.Load(SceneLoader.Scene.Login);
                });
            }
            else if (task.IsFaulted)
            {
                signInCompleted.SetException(task.Exception);
                UnityMainThread.wkr.AddJob(() =>
                {
                    SceneLoader.Load(SceneLoader.Scene.Login);
                });
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
                Debug.LogError("CANCEL");
                UnityMainThread.wkr.AddJob(() =>
                {
                    SceneLoader.Load(SceneLoader.Scene.Login);
                });
            }
            else if (task.IsFaulted)
            {
                UnityMainThread.wkr.AddJob(() =>
                {
                    SceneLoader.Load(SceneLoader.Scene.Login);
                });
                FirebaseException fbe = task.Exception.InnerExceptions[0].InnerException as FirebaseException;
                Debug.LogError(((AuthError)fbe.ErrorCode).ToString());
            }
            else
            {
                FirebaseUser newUser = task.Result;

                if (signInCompleted != null)
                    signInCompleted.SetResult(newUser);

                Debug.LogFormat("User signed in successfully: {0} ({1})",
                    newUser.DisplayName, newUser.UserId);

                playerSaveManager.LoadPlayer(newUser);

                LevelParams.Player = new PlayerData();
                LevelParams.Player.UserId = newUser.UserId;

                UnityMainThread.wkr.AddJob(() =>
                {
                    SceneLoader.Load(SceneLoader.Scene.Main);
                });
            }
        });
    }
}
