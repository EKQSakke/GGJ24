using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : Singleton<CutsceneManager>
{
    public string[] cutscenes = {
        "Cutscene_Test1",
        "Cutscene_Test2",
        "Cutscene_Test3",
    };


    string lastSceneName;

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
        }

        Debug.LogError($"Now cutscene for index {index}");
    }

    public void PlayCutscene(string sceneName)
    {
        lastSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    public void ReturnToLastScene()
    {
        SceneManager.LoadScene(lastSceneName);
    }
}

