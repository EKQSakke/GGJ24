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

    private Transform originalParent;
    private bool interacting;
    private bool hovering;

    void Awake()
    {
        Initialize();
    }

    void Initialize()
    {
        originalParent = transform.parent;
    }

    public void HoverStart(Interactor interactor)
    {
        if (hovering)
            return;

        Debug.Log("Interactable | " + gameObject.name + ": Hover Start");

        hovering = true;
        onHoverStart.Invoke(this, interactor);
    }

    public void HoverEnd(Interactor interactor)
    {
        if (!hovering)
            return;

        Debug.Log("Interactable | " + gameObject.name + ": Hover End");

        hovering = false;
        onHoverEnd.Invoke(this, interactor);
    }

    public void InteractStart(Interactor interactor, Transform parent = null)
    {
        if (interacting)
            return;

        Debug.Log("Interactable | " + gameObject.name + ": Interact Start");

        interacting = true;
        ParentInteractable(parent);
        onInteractStart.Invoke(this, interactor);
    }

    public void InteractEnd(Interactor interactor)
    {
        if (!interacting)
            return;

        Debug.Log("Interactable | " + gameObject.name + ": Interact End");

        interacting = false;
        ParentInteractable(originalParent);
        onInteractEnd.Invoke(this, interactor);
    }

    void ParentInteractable(Transform parent)
    {
        transform.SetParent(parent);

        if (parent != null)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
    }
}
