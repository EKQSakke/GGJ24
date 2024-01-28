using System.Collections;
using UnityEngine;

public class TimelineFunctions : MonoBehaviour
{
    [SerializeField]
    float soundFadeOutSpeed = 1;
    [SerializeField]
    AudioSource cutsceneMusicSource;

    public void TestMe()
    {
        DialogueDrawer.Instance.ShowText("How relaxing...");
    }

    public void EndCutscene()
    {
        CutsceneManager.Instance.ReturnToGame();
    }

    // Called from animations
    IEnumerator FadeOutMusic()
    {
        while (true)
        {
            cutsceneMusicSource.volume -= soundFadeOutSpeed * Time.deltaTime;
            yield return null;
        }
    }
}