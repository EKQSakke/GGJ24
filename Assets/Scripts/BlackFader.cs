using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BlackFader : Singleton<BlackFader>
{
    [SerializeField]
    Image image;

    [SerializeField]
    float duration = 1;

    private void Awake()
    {
        if (CreateSingleton(this, SetDontDestroy) == true)
        {
            return;
        }
    }

    void Start()
    {
        image.gameObject.SetActive(true);
        FadeIn();
    }

    public float CrossFadeScenes()
    {
        StartCoroutine(CrossFade());
        return duration;
    }

    IEnumerator CrossFade()
    {
        FadeOut();
        yield return new WaitForSeconds(duration);
        FadeIn();
    }

    public void FadeIn()
    {
        image.CrossFadeAlpha(0, duration, true);
    }

    public void FadeOut()
    {
        image.CrossFadeAlpha(1, duration, true);
    }
}
