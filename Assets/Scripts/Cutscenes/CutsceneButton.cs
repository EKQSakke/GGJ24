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
        if (SceneManager.GetActiveScene().name == "DayComplete")
            button.onClick.AddListener(GameManager.Instance.StartNewRound);



        if (SceneManager.GetActiveScene().name == "End_Loss" || SceneManager.GetActiveScene().name == "End_Win")
            SceneManager.LoadScene("StartScene");
    }
}
