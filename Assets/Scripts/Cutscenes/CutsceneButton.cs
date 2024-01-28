using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System;
using UnityEngine.Events;

public class CutsceneButton : MonoBehaviour
{
    private Button button;
    private UnityAction loadStartScene;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(CutsceneManager.Instance.ReturnToGame);
        

        Scene[] scenes = SceneManager.GetAllScenes();

        foreach (Scene scene in scenes)
        {
            if (scene.name == "DayComplete")
                button.onClick.AddListener(GameManager.Instance.StartNewRound);

            if (scene.name == "End_Loss" || scene.name == "End_Win")
            {
                loadStartScene = delegate { SceneManager.LoadScene("StartScene"); };
                button.onClick.AddListener(loadStartScene);
            }
        }
    }
}
