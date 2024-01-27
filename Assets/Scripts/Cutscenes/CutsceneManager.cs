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
        SceneManager.LoadScene(cutsceneName, LoadSceneMode.Additive);
        GameManager.Instance.CanGetStress = false;
    }

    public void ReturnToGame()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(cutsceneName));
        GameManager.Instance.CanGetStress = true;
    }
}

