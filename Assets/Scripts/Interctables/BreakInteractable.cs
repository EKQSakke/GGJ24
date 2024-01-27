using UnityEngine;

public class BreakInteractable : Interactable
{
    [SerializeField] GameObject interactionParticlePrefab;
    [SerializeField] float StressRelieved = .5f;

    public override void InteractEnd(Interactor interactor)
    {
        CutsceneManager.Instance.PlayCutscene(0);
        GameManager.Instance?.ChangeStressAmount(-StressRelieved);
        HoverEnd(interactor);
        gameObject.SetActive(false);
    }
}