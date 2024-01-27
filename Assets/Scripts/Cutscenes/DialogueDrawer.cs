using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueDrawer : Singleton<DialogueDrawer>
{
    [SerializeField]
    TextMeshProUGUI text;

    [SerializeField]
    TextMeshProUGUI speakerText;

    [SerializeField]
    float timePerCharacter = .02f;

    [SerializeField]
    float emptyDelay = 3;

    Coroutine currentRoutine;

    private void Awake()
    {
        if (CreateSingleton(this, SetDontDestroy) == true)
        {
            return;
        }
    }

    public void ShowText(string input, NPC speaker)
    {
        if (currentRoutine != null)
        {
            StopCoroutine(currentRoutine);
        }
        text.text = "";
        speakerText.text = speaker.data.name;
        currentRoutine = StartCoroutine(DrawText(input));
    }

    public void ShowText(string input) {
        speakerText.text = "";
        if (currentRoutine != null)
        {
            StopCoroutine(currentRoutine);
        }
        text.text = "";
        currentRoutine = StartCoroutine(DrawText(input));
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
        speakerText.text = "";
    }
}