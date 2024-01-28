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

    [SerializeField]
    AudioSource speakingAudioSource;

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
        if (speaker.NPCname != null)
        {
            speakerText.text = speaker.NPCname;
        }

        AudioClip npcSound = null;

        if (GameManager.Instance != null)
        {
            npcSound = GameManager.GameSettings.CustomerVoiceClips.GetRandomElementFromList();
        }

        currentRoutine = StartCoroutine(DrawText(input, npcSound));
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

    IEnumerator DrawText(string textInput, AudioClip textAudio = null)
    {
        if (speakingAudioSource  != null)
        {
            if (textAudio != null)
            {
                speakingAudioSource.clip = textAudio;
                speakingAudioSource.loop = true;
                speakingAudioSource.Play();
            }
            else
            {
                speakingAudioSource.Stop();
            }
        }

        for (int i = 0; i < textInput.Length; i++)
        {
            text.text += textInput[i];
            yield return new WaitForSeconds(timePerCharacter);
        }

        if (speakingAudioSource != null)
        {
            speakingAudioSource.loop = false;
        }

        yield return new WaitForSeconds(emptyDelay);
        text.text = "";
        speakerText.text = "";
    }

}