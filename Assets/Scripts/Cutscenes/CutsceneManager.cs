using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : Singleton<CutsceneManager>
{
    string lastSceneName;

    private void Awake()
    {
        if (CreateSingleton(this, SetDontDestroy) == true)
        {
            return;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayCutscene("Sakke_Cutscene_Test1");
        }
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

