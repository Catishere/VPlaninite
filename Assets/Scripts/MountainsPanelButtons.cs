using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MountainsPanelButtons : MonoBehaviour
{
    public Button backButton;
    // Start is called before the first frame update
    private void Awake()
    {
        backButton.onClick.AddListener(() =>
        {
            SceneLoader.Load(SceneLoader.Scene.Main);
        });

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Mountain"))
        {
            Button tree = obj.GetComponent<Button>();
            tree.onClick.AddListener(() =>
            {
                LevelParams.Mountain = tree.transform.Find("Text").GetComponent<Text>().text;
                SceneLoader.Load(SceneLoader.Scene.Levels);
            });
        }
    }
}
