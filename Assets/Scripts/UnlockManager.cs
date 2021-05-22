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
        int level = int.Parse(levelText.text);
        if (level != 1 && LevelParams.Player?.LevelReached < level)
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
        Debug.Log(level + " " + LevelParams.Player?.LevelReached);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
