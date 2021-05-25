using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockManager : MonoBehaviour
{
    public Text levelText;
    // Start is called before the first frame update
    void Start()
    {
        int mountainTopLevel = -1;
        int level = int.Parse(levelText.text);

        if (level == 1) return;

        if (LevelParams.Player == null || string.IsNullOrEmpty(LevelParams.Mountain))
        {
            gameObject.GetComponent<Button>().interactable = false;
            return;
        }

        var mountainIndex = LevelParams.Player.LevelReachedKeys.IndexOf(LevelParams.Mountain);
        if (mountainIndex >= 0)
            mountainTopLevel = LevelParams.Player.LevelReachedValues[mountainIndex];

        if (mountainIndex < 0 || mountainTopLevel < level)
            gameObject.GetComponent<Button>().interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
