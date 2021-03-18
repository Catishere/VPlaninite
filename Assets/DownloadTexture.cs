using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public static class DownloadTexture 
{
    public static IEnumerator downloadImage(string url, Image image)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        DownloadHandler handle = www.downloadHandler;

        //Send Request and wait
        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.ConnectionError 
            || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Error while Receiving: " + www.error);
        }
        else
        {
            Debug.Log("Image downloaded successfully.");

            Texture2D texture2d = DownloadHandlerTexture.GetContent(www);

            Sprite sprite = null;
            sprite = Sprite.Create(texture2d, new Rect(0, 0, texture2d.width, texture2d.height), Vector2.zero);

            if (sprite != null)
            {
                image.sprite = sprite;
            }
        }
    }
}