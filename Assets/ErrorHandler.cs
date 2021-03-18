using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;

public static class ErrorHandler
{
    public static void displayErrorOnObject(GameObject popUp, AuthError errorCode)
    {
        UnityMainThread.wkr.AddJob(() =>
        {
            popUp.transform.Find("Panel/ErrorText").GetComponent<Text>().text = GetErrorMessage(errorCode);
            popUp.SetActive(true);
        });
    }

    public static void displayMessageOnObject(GameObject popUp, string message)
    {
        UnityMainThread.wkr.AddJob(() =>
        {
            popUp.transform.Find("Panel/ErrorText").GetComponent<Text>().text = message;
            popUp.SetActive(true);
        });
    }

    public static string GetErrorMessage(AuthError errorCode)
    {
        var message = "";
        switch (errorCode)
        {
            case AuthError.AccountExistsWithDifferentCredentials:
                message = "Въведените от вас данни са грешни.";
                break;
            case AuthError.MissingPassword:
                message = "Моля въведете парола.";
                break;
            case AuthError.WeakPassword:
                message = "Вашата парола е слаба.";
                break;
            case AuthError.WrongPassword:
                message = "Въведените от вас данни са грешни.";
                break;
            case AuthError.EmailAlreadyInUse:
                message = "Този E-mail адрес вече се използва.";
                break;
            case AuthError.InvalidEmail:
                message = "Въведените от вас E-mail адрес е невалиден.";
                break;
            case AuthError.MissingEmail:
                message = "Моля въведете E-mail адрес.";
                break;
            case AuthError.UserNotFound:
                message = "Въведените от вас данни са грешни.";
                break;
            default:
                message = "Грешка: " + errorCode.ToString();
                break;
        }
        return message;
    }
}
