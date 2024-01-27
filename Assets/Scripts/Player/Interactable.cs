using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private UnityEvent<Interactable, Interactor> onInteractStart; public UnityEvent<Interactable, Interactor> OnInteractStart { get { return onInteractStart; } }
    [SerializeField] private UnityEvent<Interactable, Interactor> onInteractEnd; public UnityEvent<Interactable, Interactor> OnInteractEnd { get { return onInteractEnd; } }
    [SerializeField] private UnityEvent<Interactable, Interactor> onHoverStart; public UnityEvent<Interactable, Interactor> OnHoverStart { get { return onHoverStart; } }
    [SerializeField] private UnityEvent<Interactable, Interactor> onHoverEnd; public UnityEvent<Interactable, Interactor> OnHoverEnd { get { return onHoverEnd; } }

    private bool interacting;
    private bool hovering;

    public void HoverStart(Interactor interactor)
    {
        if (hovering)
            return;

        hovering = true;
        Debug.Log("Interactable | " + gameObject.name + ": Hover Start");
        onHoverStart.Invoke(this, interactor);
    }

    public void HoverEnd(Interactor interactor)
    {
        if (!hovering)
            return;

        hovering = false;
        Debug.Log("Interactable | " + gameObject.name + ": Hover End");
        onHoverEnd.Invoke(this, interactor);
    }

    public void InteractStart(Interactor interactor)
    {
        if (interacting)
            return;
            
        interacting = true;
        Debug.Log("Interactable | " + gameObject.name + ": Interact Start");
        onInteractStart.Invoke(this, interactor);
    }

    public void InteractEnd(Interactor interactor)
    {
        if (!interacting)
            return;

        interacting = false;
        Debug.Log("Interactable | " + gameObject.name + ": Interact End");
        onInteractEnd.Invoke(this, interactor);
    }
}
