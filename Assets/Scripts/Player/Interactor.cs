using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform interactorBase;
    [SerializeField] private Transform interactableHolder;
    [SerializeField] private LayerMask interactableLayers;
    [SerializeField] private float interactorDistance = 3f;

    private List<Interactable> hoverInteractables = new List<Interactable>();
    private List<Interactable> lastHoverInteractables = new List<Interactable>();

    private bool interacting;
    private Interactable interactable;

    void Update()
    {
        CollectInteractables();
        ClearInteractables();
    }

    void CollectInteractables()
    {
        hoverInteractables.Clear();

        if (!interacting)
        {
            if (Physics.Raycast(interactorBase.position, interactorBase.forward, out RaycastHit rayhit, interactorDistance, interactableLayers, QueryTriggerInteraction.Ignore))
            {
                if (rayhit.collider.TryGetComponent(out Interactable interactable))
                {
                    hoverInteractables.Add(interactable);
                    interactable.HoverStart(this);
                }
            }
        }
    }

    void ClearInteractables()
    {
        for (int i = 0; i < lastHoverInteractables.Count; i++)
        {
            if (!hoverInteractables.Contains(lastHoverInteractables[i]))
                lastHoverInteractables[i].HoverEnd(this);
        }

        lastHoverInteractables.Clear();
        lastHoverInteractables.AddRange(hoverInteractables);
    }

    public void StartInteraction()
    {
        if (interacting || hoverInteractables.Count == 0)
            return;

        interacting = true;
        interactable = hoverInteractables[0];
        interactable.InteractStart(this);
    }

    public void EndInteraction()
    {
        if (!interacting || interactable == null)
            return;

        interacting = false;
        interactable.InteractEnd(this);
    }
}
