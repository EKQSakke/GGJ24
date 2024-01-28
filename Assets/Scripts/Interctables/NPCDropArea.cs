using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDropArea : Interactable
{
    public override InteractionMode InteractionType => InteractionMode.None;

    public NPCSpawner spawner;

    public override void InteractableUsedOnMe(Interactable interactable)
    {
        base.InteractableUsedOnMe(interactable);

        DialogueDrawer.Instance.StopDialogue();

        if (SoundEffectManager.instance != null)
            SoundEffectManager.instance.PlaySoundEffectBank("GiveSound");

        NPC.CurrentNPCAtDesk?.UseItemOnCurrentNPC(interactable.DataObject);
    }
}