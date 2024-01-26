using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueDrawer : MonoBehaviour
{
    public static DialogueDrawer instance;

    [SerializeField]
    TextMeshProUGUI text;

    [SerializeField]
    float timePerCharacter = .02f;

    [SerializeField]
    float emptyDelay = 3;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
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