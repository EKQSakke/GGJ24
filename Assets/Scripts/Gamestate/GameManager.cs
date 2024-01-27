using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class GameManager : Singleton<GameManager>
{

    public static Action RoundStart;
    public static Action RoundOver;

    [Header("Game settings")]
    public List<GameRoundSettings> GameRounds = new List<GameRoundSettings>();

    [Header("References")]
    public NPCSpawner QueSpawner;
    public List<UsableItemSpawner> ItemSpawners;
    public Image GameClock;
    public Slider StressLevel;
    public TextMeshProUGUI DayTitleText;
    public GameObject DayOverUI;
    public GameObject PaperDropArea;
    public GameObject NPCDropArea;

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

        GameData.LoadDataFiles();        
    }

    private void Start()
    {
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

    #region Round handling

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

        List<UsableItemData> items = new List<UsableItemData>(currentRoundSettings.ItemsToSpawn);

        foreach (UsableItemSpawner spawner in currentRoundSettings.UsedSpawnersForRound)
        {
            spawner.SetSpawnerInteractableState(true);
            spawner.CreateItems(items);
        }

        QueSpawner.SpawnNPCs(10);

        PaperDropArea.gameObject.SetActive(currentRoundSettings.UseNPCDropArea == false);
        NPCDropArea.gameObject.SetActive(currentRoundSettings.UseNPCDropArea);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        GameCurrentlyActive = true;
        RoundStart?.Invoke();
    }

    private void EndCurrentRound()
    {
        Debug.Log("Ending game round: " + currentGameRound);
        currentGameRound++;

        foreach (UsableItemSpawner spawner in currentRoundSettings.UsedSpawnersForRound)
        {
            spawner.SetSpawnerInteractableState(false);
        }

        RoundOver?.Invoke();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

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
        ChangeStressAmount(currentRoundSettings.DefaultStressPerSecond * Time.deltaTime);
    }

    #endregion

    public void ChangeStressAmount(float changeBy)
    {
        currentStressLevel = Mathf.Clamp01(currentStressLevel + changeBy);

        if (currentStressLevel > currentRoundSettings.StressThreshold)
        {
            Debug.LogError("STRESSED OUT!");
            currentStressLevel = 0f;
        }

        StressLevel.value = currentStressLevel;
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
    public bool UseNPCDropArea = true;
    public List<UsableItemSpawner> UsedSpawnersForRound;
    public List<UsableItemData> ItemsToSpawn;
}