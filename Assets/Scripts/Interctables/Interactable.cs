using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public virtual InteractionMode InteractionType => myData != null ? myData.InteractionMode : InteractionMode.Drag;
    public UsableItemType ItemType => myData != null ? myData.ItemType : UsableItemType.None;
    public UsableItemData DataObject => myData;

    [Header("Events")]
    [SerializeField] private UsableItemData DefaultDataToUse;
    [SerializeField] private UnityEvent<Interactable, Interactor> onInteractStart; public UnityEvent<Interactable, Interactor> OnInteractStart { get { return onInteractStart; } }
    [SerializeField] private UnityEvent<Interactable, Interactor> onInteractEnd; public UnityEvent<Interactable, Interactor> OnInteractEnd { get { return onInteractEnd; } }
    [SerializeField] private UnityEvent<Interactable, Interactor> onHoverStart; public UnityEvent<Interactable, Interactor> OnHoverStart { get { return onHoverStart; } }
    [SerializeField] private UnityEvent<Interactable, Interactor> onHoverEnd; public UnityEvent<Interactable, Interactor> OnHoverEnd { get { return onHoverEnd; } }

    protected Transform originalParent;
    protected bool interacting;
    protected bool hovering;
    protected UsableItemData myData;

    protected virtual void Awake()
    {
        Initialize();
    }
    
    protected virtual void Update()
    {

    }

    private void Initialize()
    {
        originalParent = transform.parent;
        SetupInteractable(DefaultDataToUse);
    }

    public virtual void SetupInteractable(UsableItemData data)
    {
        myData = data;
    }

    public virtual void HoverStart(Interactor interactor)
    {
        if (hovering)
            return;

        Debug.Log("Interactable | " + gameObject.name + ": Hover Start");

        hovering = true;
        onHoverStart.Invoke(this, interactor);
    }

    public virtual void HoverEnd(Interactor interactor)
    {
        if (!hovering)
            return;

        Debug.Log("Interactable | " + gameObject.name + ": Hover End");

        hovering = false;
        onHoverEnd.Invoke(this, interactor);
    }

    public virtual void InteractStart(Interactor interactor, Transform parent = null)
    {
        if (interacting)
            return;

        Debug.Log("Interactable | " + gameObject.name + ": Interact Start");

        interacting = true;

        if (InteractionType == InteractionMode.Drag)
            ParentInteractable(parent);

        onInteractStart.Invoke(this, interactor);
    }

    public virtual void InteractEnd(Interactor interactor)
    {
        if (!interacting)
            return;

        Debug.Log("Interactable | " + gameObject.name + ": Interact End");

        interacting = false;

        if (InteractionType == InteractionMode.Drag)
            ParentInteractable(originalParent);

        onInteractEnd.Invoke(this, interactor);
    }

    public virtual void InteractableUsedOnMe(Interactable interactable)
    {
        Debug.Log("Interactable | " + interactable.gameObject.name + ": Used on me: " + gameObject.name);
    }

    private void ParentInteractable(Transform parent)
    {
        transform.SetParent(parent);

        if (parent != null)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
    }
}
