using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeInteractable : Interactable
{
    public float StressRelieved = 0.25f;

    public override void InteractEnd(Interactor interactor)
    {
        base.InteractEnd(interactor);

        GameManager.Instance?.ChangeStressAmount(-StressRelieved);
    }

}