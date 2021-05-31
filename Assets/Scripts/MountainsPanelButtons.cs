using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MountainsPanelButtons : MonoBehaviour
{
    public Button backButton;

    public Sprite[] sprites;
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
            var quizScoreIndex =
                LevelParams.Player.QuizScoreKeys.IndexOf(tree.transform.Find("Text").GetComponent<Text>().text);
            if (quizScoreIndex >= 0)
            {
                var quizScore = LevelParams.Player.QuizScoreValues[quizScoreIndex];
                if (quizScore == 6)
                    obj.transform.Find("Stars").GetComponent<Image>().sprite = sprites[2];
                else if (quizScore >= 4)
                    obj.transform.Find("Stars").GetComponent<Image>().sprite = sprites[1];
                else if (quizScore >= 2)
                    obj.transform.Find("Stars").GetComponent<Image>().sprite = sprites[0];
            }
            tree.onClick.AddListener(() =>
            {
                LevelParams.Mountain = tree.transform.Find("Text").GetComponent<Text>().text;
                SceneLoader.Load(SceneLoader.Scene.Levels);
            });
        }
    }
}
