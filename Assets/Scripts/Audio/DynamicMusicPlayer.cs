using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource basePlayer;
    [SerializeField] private AudioSource overlayPlayer;
    [SerializeField] private bool playOnStart = true;
    [SerializeField] private bool loopMusic = true;
    [SerializeField] private AnimationCurve overlayBlendCurve;
    
    private bool isPlaying;

    void OnValidate()
    {
        if (basePlayer != null)
            basePlayer.playOnAwake = false;

        if (overlayPlayer != null)
            overlayPlayer.playOnAwake = false;
    }

    void Start()
    {
        if (playOnStart)
            PlayMusic();
    }

    [ContextMenu(nameof(PlayMusic))]
    public void PlayMusic()
    {
        if (isPlaying)
            return;

        isPlaying = true;
        basePlayer.loop = true;
        overlayPlayer.loop = true;
        basePlayer.Play();
        overlayPlayer.Play();

        StopAllCoroutines();

        if (gameObject.activeInHierarchy)
            StartCoroutine(EvaluateMusic());
    }

    [ContextMenu(nameof(StopMusic))]
    public void StopMusic()
    {
        if (!isPlaying)
            return;

        basePlayer.Stop();
        overlayPlayer.Stop();
        isPlaying = false;
        StopAllCoroutines();
    }

    [ContextMenu(nameof(PauseMusic))]
    public void PauseMusic()
    {
        if (!isPlaying)
            return;

        basePlayer.Pause();
        overlayPlayer.Pause();
        isPlaying = false;
        StopAllCoroutines();
    }

    public void TogglePause()
    {
        if (isPlaying)
        {
            PauseMusic();
        }
        else
        {
            PlayMusic();
        }
    }

    IEnumerator EvaluateMusic()
    {
        float dynamicLevel = 0;

        while (isPlaying)
        {
            dynamicLevel = GameManager.Instance.Stress;
            overlayPlayer.volume = overlayBlendCurve.Evaluate(dynamicLevel);

            yield return null;
        }
    }
}
