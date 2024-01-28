using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : Singleton<CutsceneManager>
{
    public string[] cutscenes = {
        "Cutscene_Test1",
        "Cutscene_Test2",
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
        PlayCutscene(cutscenes[index % 2]);
    }

    public void PlayCutscene(string sceneName)
    {
        cutsceneName = sceneName;
        StartCoroutine(StartCutscene());
    }

    IEnumerator StartCutscene()
    {
        GameManager.Instance.OnBreak = true;
        var crossFadeDuration = BlackFader.Instance.CrossFadeScenes();
        yield return new WaitForSeconds(crossFadeDuration);
        SceneManager.LoadScene(cutsceneName, LoadSceneMode.Additive);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ReturnToGame()
    {
        StartCoroutine(EndCutscene());
    }

    IEnumerator EndCutscene()
    {
        var crossFadeDuration = BlackFader.Instance.CrossFadeScenes();
        yield return new WaitForSeconds(crossFadeDuration);
        if (cutsceneName != "")
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(cutsceneName));
        }
        else
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        }

        GameManager.Instance.OnBreak = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
}

