using UnityEngine;

public class BreakInteractable : Interactable
{
    [SerializeField] GameObject interactionParticlePrefab;
    [SerializeField] float StressRelieved = .5f;

    public override void InteractEnd(Interactor interactor)
    {
        GameManager.Instance?.ChangeStressAmount(-StressRelieved);
        FindAnyObjectByType<CoffeeInteractable>().Refill();
        CutsceneManager.Instance.PlayCutscene(0);
        HoverEnd(interactor);
        gameObject.SetActive(false);
    }
}