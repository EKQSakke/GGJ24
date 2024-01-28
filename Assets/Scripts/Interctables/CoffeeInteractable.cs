using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeInteractable : Interactable
{
    [SerializeField] GameObject interactionParticlePrefab;
    [SerializeField] private ParticleSystem coffeeLeftParticles;

    public float StressRelieved = 0.25f;
    public int CoffeeAmount = 3;
    public int MaxCoffeeAmount = 3;

    public override void InteractEnd(Interactor interactor)
    {
        if (CoffeeAmount < 1)
        {
            Debug.Log("Out of coffee!");
            return;
        }

        CoffeeAmount--;

        base.InteractEnd(interactor);

        GameManager.Instance?.ChangeStressOverTime(-StressRelieved);

        if (interactionParticlePrefab != null)
        {
            GameObject interactionParticle = Instantiate(interactionParticlePrefab, transform.position, Quaternion.identity);
            Destroy(interactionParticle, 2f);
        }

        if (CoffeeAmount <= 0)
            coffeeLeftParticles.Stop();
    }

    public void Refill(int amount = 3)
    {
        if (CoffeeAmount + amount > MaxCoffeeAmount)
        {
            CoffeeAmount = MaxCoffeeAmount;
            return;
        }
        CoffeeAmount = amount;
    }
}