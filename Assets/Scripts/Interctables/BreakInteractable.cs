using UnityEngine;

public class BreakInteractable : Interactable
{
    [SerializeField] GameObject interactionParticlePrefab;
    [SerializeField] float StressRelieved = .5f;

    void Start()
    {
        GameManager.RoundStart += () => gameObject.SetActive(true);
    }

    public override void InteractEnd(Interactor interactor)
    {
        GameManager.Instance?.ChangeStressOverTime(-StressRelieved);
        FindAnyObjectByType<CoffeeInteractable>().Refill();
        CutsceneManager.Instance.PlayCutscene(GameManager.Instance.CurrentGameRound);
        HoverEnd(interactor);
        gameObject.SetActive(false);
    }
}