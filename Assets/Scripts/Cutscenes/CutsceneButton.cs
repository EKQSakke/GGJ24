using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutsceneButton : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(CutsceneManager.Instance.ReturnToGame);

        for (int i = 0; i < SceneManager.loadedSceneCount; i++)
        {
            string sceneName = SceneManager.GetSceneAt(i).name;

            if (sceneName == "DayComplete")
                button.onClick.AddListener(GameManager.Instance.StartNewRound);
            if (sceneName == "End_Loss" || sceneName == "End_Win")
                SceneManager.LoadScene("StartScene");
        }        
    }
}
