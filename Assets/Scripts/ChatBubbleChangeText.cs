using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatBubbleChangeText : MonoBehaviour
{
    public Text txt;
    // Start is called before the first frame update
    void Start()
    {
        
        txt.text = "Здравей " + LevelParams.Player + ". Готов ли си за приключения? ";
        StartCoroutine(Timedelay());

    }

    // Update is called once per frame
    void Update()
    {
      
    }

    IEnumerator Timedelay() {
        yield return new WaitForSeconds(3);
        txt.text = "Ще имам нужда от помощта ти за изкачването на върховете.";
        yield return new WaitForSeconds(3);
        txt.text = "Когато си готов, отиди на \"Пътешествия\" и нека започнем изкачването.";
    }
}
