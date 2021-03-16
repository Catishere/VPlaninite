using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyCanvas : MonoBehaviour
{
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        button.onClick.AddListener(() => {
            gameObject.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
