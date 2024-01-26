using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public List<GameRoundSettings> GameRounds = new List<GameRoundSettings>();
    public Image GameClock;
    public Slider StressLevel;
    public TextMeshProUGUI DayTitleText;
    public GameObject DayOverUI;

    internal bool GameCurrentlyActive = false;
    internal float Stress => currentStressLevel;

    private GameRoundSettings currentRoundSettings => GameRounds[currentGameRound];

    private int currentGameRound = 0;
    private float currentRoundTime = 0;
    private float currentStressLevel = 0;

    // Start is called before the first frame update
    private void Awake()
    {
        if (CreateSingleton(this, SetDontDestroy) == false)
        {
            return;
        }

        StartNewGame();
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameCurrentlyActive == false)
            return;

        currentRoundTime += Time.deltaTime;

        if (currentRoundTime >= currentRoundSettings.TimeInSeconds)
        {
            EndCurrentRound();
        }
        else
        {
            UpdateGameState();
        }        
    }

    private void StartNewGame()
    {
        currentGameRound = 0;
        StartNewRound();
    }

    private void EndGame()
    {
        GameCurrentlyActive = false;
        Debug.Log("Game over!");
    }

    public void StartNewRound()
    {
        Debug.Log("Starting new game round: " + currentGameRound);

        currentRoundTime = 0f;
        currentStressLevel = 0f;

        GameClock.fillAmount = 0;
        DayTitleText.text = "Day " + (currentGameRound + 1);
        SetDayOverUI(false);

        GameCurrentlyActive = true;
    }

    private void EndCurrentRound()
    {
        Debug.Log("Ending game round: " + currentGameRound);
        currentGameRound++;

        if (currentGameRound >= GameRounds.Count)
        {
            EndGame();
        }
        else
        {
            GameCurrentlyActive = false;
            SetDayOverUI(true);
        }
    }

    private void UpdateGameState()
    {
        GameClock.fillAmount = currentRoundTime / currentRoundSettings.TimeInSeconds;
        StressLevel.value = currentStressLevel;

        if (currentStressLevel > currentRoundSettings.StressThreshold)
        {
            Debug.LogError("STRESSED OUT!");
            currentStressLevel = 0f;
        }
        else
        {
            currentStressLevel += currentRoundSettings.DefaultStressPerSecond * Time.deltaTime;
        }
    }

    private void SetDayOverUI(bool setTo)
    {
        DayOverUI.SetActive(setTo);
    }

}

[Serializable]
public class GameRoundSettings
{
    public float TimeInSeconds;
    [Range(0f, 1f)]
    public float StressThreshold = 0.75f;
    [Range(0f, 1f)]
    public float DefaultStressPerSecond = 0.15f;
}