using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableContainer : Interactable
{
    [Serializable]
    public class ContainerState
    {
        public string AnimatorTriggerToSet;
        public float timeToWaitForAnimation;
        public bool InteractablesInContainerEnabled = true;
    }

    public Action ContainerStateChanged;

    public Animator Animator;
    public List<ContainerState> ContainerStates = new List<ContainerState>();
    
    public override InteractionMode InteractionType => currentlyAnimating ? InteractionMode.None : base.InteractionType;

    public bool ContainerInteractablesAreEnabled => currentState.InteractablesInContainerEnabled;

    private ContainerState currentState => ContainerStates[currentStateIndex];
    private float animationTime => currentState.timeToWaitForAnimation;

    private float timeAnimated = 0f;
    private int currentStateIndex = 0;
    private bool currentlyAnimating = false;

    public override void InteractEnd(Interactor interactor)
    {
        Debug.Log("set trigger: " + ContainerStates[currentStateIndex].AnimatorTriggerToSet);

        timeAnimated = 0;
        currentStateIndex++;

        if (currentStateIndex >= ContainerStates.Count)
        {
            currentStateIndex = 0;
        }

        if (string.IsNullOrEmpty(currentState.AnimatorTriggerToSet) == false)
        {
            Animator.SetTrigger(currentState.AnimatorTriggerToSet);
        }

        if (currentState.timeToWaitForAnimation > 0)
        {
            currentlyAnimating = true;
        }
        else
        {
            currentlyAnimating = false;            
        }

        ContainerStateChanged?.Invoke();
        base.InteractEnd(interactor);
    }

    protected override void Update()
    {
        base.Update();

        if (currentlyAnimating)
        {
            timeAnimated += Time.deltaTime;

            if (timeAnimated > animationTime) 
            {
                currentlyAnimating = false;
            }
        }
    }

}