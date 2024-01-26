using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueDrawer : Singleton<DialogueDrawer>
{
    [SerializeField]
    TextMeshProUGUI text;

    [SerializeField]
    float timePerCharacter = .02f;

    [SerializeField]
    float emptyDelay = 3;

    private void Awake()
    {
        if (CreateSingleton(this, SetDontDestroy) == true)
        {
            return;
        }
    }

    public void ShowText(string text)
    {
        StartCoroutine(DrawText(text));
    }

    IEnumerator DrawText(string textInput)
    {
        for (int i = 0; i < textInput.Length; i++)
        {
            text.text += textInput[i];
            yield return new WaitForSeconds(timePerCharacter);
        }

        yield return new WaitForSeconds(emptyDelay);
        text.text = "";
    }
}