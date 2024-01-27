using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Camera interactorCamera;
    [SerializeField] private RectTransform interactorCrosshair;
    
    [Space]
    [SerializeField] private Transform interactableHolder;
    [SerializeField] private Transform interactableHolderStart;
    [SerializeField] private Transform interactableHolderEnd;
    [SerializeField] private AnimationCurve interactableHolderCurve;

    [Space]
    [SerializeField] private LayerMask interactableLayers;
    [SerializeField] private float interactorDistance = 3f;
    [SerializeField] private bool holdInteractionMode = true;

    [Header("Events")]
    [SerializeField] private UnityEvent<Interactable, Interactor> onInteractStart; public UnityEvent<Interactable, Interactor> OnInteractStart { get { return onInteractStart; } }
    [SerializeField] private UnityEvent<Interactable, Interactor> onInteractEnd; public UnityEvent<Interactable, Interactor> OnInteractEnd { get { return onInteractEnd; } }

    private List<Interactable> hoverInteractables = new List<Interactable>();
    private List<Interactable> lastHoverInteractables = new List<Interactable>();

    private bool interacting;
    private Interactable interactable;

    private Coroutine interactableHolderMoveRoutine;

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
            Interactable interactable = ScanForInteractable();

            if (interactable != null)
            {
                hoverInteractables.Add(interactable);
                interactable.HoverStart(this);
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
        if (this.holdInteractionMode && this.interactable != null)
        {
            StopInteraction();
        }
        else
        {
            if (interacting || hoverInteractables.Count == 0)
                return;

            if (hoverInteractables[0].InteractionType == InteractionMode.None)
                return;

            interactable = hoverInteractables[0];
            interactable.InteractStart(this, interactableHolder);

            if (interactable.InteractionType == InteractionMode.Click)
            {
                StopInteraction();
            }
            else
            {
                interacting = true;

                if (interactableHolderMoveRoutine != null)
                    StopCoroutine(interactableHolderMoveRoutine);
                if (gameObject.activeInHierarchy)
                    interactableHolderMoveRoutine = StartCoroutine(InteractableHolderMoveRoutine());

                onInteractStart.Invoke(interactable, this);
            }            
        }
    }

    public void EndInteraction()
    {
        if (!interacting || interactable == null)
            return;

        if (this.holdInteractionMode == false)
        {
            StopInteraction();
        }
    }

    private void StopInteraction()
    {
        if (interactable.InteractionType == InteractionMode.Drag)
        {
            Interactable hoverTarget = ScanForInteractable();

            if (hoverTarget != null)
            {
                hoverTarget.InteractableUsedOnMe(interactable);
            }
        }

        interacting = false;
        interactable.InteractEnd(this);
        interactable = null;

        onInteractEnd.Invoke(interactable, this);
    }

    private Interactable ScanForInteractable()
    {
        Ray ray = interactorCamera.ScreenPointToRay(interactorCrosshair.position);

        if (Physics.Raycast(ray, out RaycastHit rayhit, interactorDistance, interactableLayers, QueryTriggerInteraction.Ignore))
        {
            if (rayhit.collider.TryGetComponent(out Interactable interactable))
            {
                return interactable;
            }
        }

        return null;
    }

    IEnumerator InteractableHolderMoveRoutine()
    {
        float timer = 0f;
        float duration = interactableHolderCurve.keys[interactableHolderCurve.length - 1].time;
        float evaluatedValue = 0f;

        while (timer <= duration)
        {
            evaluatedValue = interactableHolderCurve.Evaluate(timer / duration);
            interactableHolder.position = Vector3.LerpUnclamped(interactableHolderStart.position, interactableHolderEnd.position, evaluatedValue);
            interactableHolder.rotation = Quaternion.LerpUnclamped(interactableHolderStart.rotation, interactableHolderEnd.rotation, evaluatedValue);

            timer += Time.deltaTime;
            yield return null;
        }
    }

}
