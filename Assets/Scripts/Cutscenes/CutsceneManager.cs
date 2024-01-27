using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : Singleton<CutsceneManager>
{
    public string[] cutscenes = {
        "Cutscene_Test1",
        "Cutscene_Test2",
        "Cutscene_Test3",
    };

    string cutsceneName;

    private void Awake()
    {
        if (CreateSingleton(this, SetDontDestroy) == true)
        {
            return;
        }
    }

    public void PlayCutscene(int index)
    {
        if (index < cutscenes.Length)
        {
            PlayCutscene(cutscenes[index]);
            return;
        }

        Debug.Log($"No cutscene for index {index}");
        PlayCutscene(cutscenes[0]);
    }

    public void PlayCutscene(string sceneName)
    {
        cutsceneName = sceneName;
        StartCoroutine(StartCutscene());
    }

    IEnumerator StartCutscene()
    {
        GameManager.Instance.CanGetStress = false;
        var crossFadeDuration = BlackFader.Instance.CrossFadeScenes();
        yield return new WaitForSeconds(crossFadeDuration);
        SceneManager.LoadScene(cutsceneName, LoadSceneMode.Additive);
    }

    public void ReturnToGame()
    {
        StartCoroutine(EndCutscene());
    }
    
    IEnumerator EndCutscene()
    {
        var crossFadeDuration = BlackFader.Instance.CrossFadeScenes();
        yield return new WaitForSeconds(crossFadeDuration);
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(cutsceneName));
        GameManager.Instance.CanGetStress = true;
    }

}

