using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextDrawer : MonoBehaviour
{
    string textToDraw;

    TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        textToDraw = text.text;
        text.text = "";
        StartCoroutine(DrawText());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator DrawText()
    {
        var count = textToDraw.Length;

        for (int i = 0; i < count; i++)
        {
            text.text += textToDraw[i];
            yield return new WaitForSeconds(.02f);
        }

        yield return new WaitForSeconds(3);
        text.text = "";
    }
}
