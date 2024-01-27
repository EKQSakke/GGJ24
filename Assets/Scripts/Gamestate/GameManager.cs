using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{

    public static Action RoundStart;
    public static Action RoundOver;

    public static BasicGameSettings GameSettings => Instance.CurrentGameSettings;

    [Header("Game settings")]
    public List<GameRoundSettings> GameRounds = new List<GameRoundSettings>();
    public BasicGameSettings CurrentGameSettings;

    [Header("References")]
    public NPCSpawner QueSpawner;
    public NPCMover QueMover;
    public List<UsableItemSpawner> ItemSpawners;
    public Image GameClock;
    public Slider StressLevel;
    public TextMeshProUGUI DayTitleText;
    public GameObject DayOverUI;
    public GameObject PaperDropArea;
    public GameObject NPCDropArea;

    internal bool GameCurrentlyActive = false;
    internal bool CanGetStress = true;
    internal float Stress => currentStressLevel;

    private GameRoundSettings currentRoundSettings => GameRounds[currentGameRound];

    private int currentGameRound = 0; public int CurrentGameRound { get { return currentGameRound; } }
    private float currentRoundTime = 0;
    private float currentStressLevel = 0;

    private float timeWithCurrentNpc = 0;
    [SerializeField]
    private float timeToFullNpcStressMultiplier = 10;

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

        QueSpawner.SpawnNPCs(QueMover.AmountOfQueuePoints);

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

        if (currentGameRound == GameRounds.Count - 1)
        {
            CutsceneManager.Instance.PlayCutscene("End_Win");
            return;
        }

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
        timeWithCurrentNpc += Time.deltaTime;
        var npcStressMultiplier = GetNpcStressMultiplier();
        GameClock.fillAmount = currentRoundTime / currentRoundSettings.TimeInSeconds;
        ChangeStressAmount(currentRoundSettings.DefaultStressPerSecond * npcStressMultiplier * Time.deltaTime);
    }

    #endregion

    public void ChangeStressAmount(float changeBy)
    {
        if (!CanGetStress)
            return;

        currentStressLevel = Mathf.Clamp01(currentStressLevel + changeBy);

        if (currentStressLevel > currentRoundSettings.StressThreshold)
        {
            Debug.LogError("STRESSED OUT!");
            CutsceneManager.Instance.PlayCutscene("End_Loss");
            currentStressLevel = 0f;
        }

        StressLevel.value = currentStressLevel;
    }

    private void SetDayOverUI(bool setTo)
    {
        DayOverUI.SetActive(setTo);
    }

    public void ResetNpcTimer()
    {
        timeWithCurrentNpc = 0;
    }

    float GetNpcStressMultiplier()
    {
        var npcData = NPC.CurrentNPCAtDesk.data;
        var evalPoint = timeWithCurrentNpc / timeToFullNpcStressMultiplier;
        return npcData.StressGenerationCurve.Evaluate(evalPoint);
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
    public DayRules DayRules;
}