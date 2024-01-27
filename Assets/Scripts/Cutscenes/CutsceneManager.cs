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

        Debug.LogError($"No cutscene for index {index}");
    }

    public void PlayCutscene(string sceneName)
    {
        cutsceneName = sceneName;
        SceneManager.LoadScene(cutsceneName, LoadSceneMode.Additive);
    }

    public void ReturnToGame()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(cutsceneName)).completed += (_) => GameManager.Instance.StartNewRound();
    }
}

