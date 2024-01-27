using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableContainer : Interactable
{
    [Serializable]
    public class ContainerState
    {
        public bool MoveContainerToPosition = false;
        public Vector3 PositionOffset = Vector3.zero;
        public float timeToWaitForAnimation;
        public bool InteractablesInContainerEnabled = true;
    }

    public Action ContainerStateChanged;

    public Transform MovableTransform;
    public List<ContainerState> ContainerStates = new List<ContainerState>();
    
    public override InteractionMode InteractionType => currentlyAnimating ? InteractionMode.None : base.InteractionType;

    public bool ContainerInteractablesAreEnabled => currentState.InteractablesInContainerEnabled;

    private ContainerState currentState => ContainerStates[currentStateIndex];
    private float animationTime => currentState.timeToWaitForAnimation;

    private Vector3? defaultPos;
    private int currentStateIndex = 0;
    private bool currentlyAnimating = false;
        
    public override void SetupInteractable(UsableItemData data)
    {
        base.SetupInteractable(data);
        defaultPos = MovableTransform.localPosition;
    }

    public override void InteractEnd(Interactor interactor)
    {
        currentStateIndex++;

        if (currentStateIndex >= ContainerStates.Count)
        {
            currentStateIndex = 0;
        }

        if (currentState.timeToWaitForAnimation > 0)
        {
            StartCoroutine(AnimateToPosition());            
        }
        else
        {
            currentlyAnimating = false;            
        }

        ContainerStateChanged?.Invoke();
        base.InteractEnd(interactor);
    }
    
    private IEnumerator AnimateToPosition()
    {
        currentlyAnimating = true;
        float timeAnimated = 0f;
        Vector3 startPos = MovableTransform.localPosition;
        Vector3 endPos = (Vector3)defaultPos + currentState.PositionOffset;

        while (timeAnimated < animationTime)
        {
            yield return null;
            timeAnimated += Time.deltaTime;
            MovableTransform.localPosition = Vector3.Lerp(startPos, endPos, timeAnimated / animationTime);
        }

        MovableTransform.localPosition = endPos;
        currentlyAnimating = false;
    }


}