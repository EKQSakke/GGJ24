using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private static NPC currentNPCAtDesk; public static NPC CurrentNPCAtDesk { get { return currentNPCAtDesk; } }

    public delegate void NPCAction(NPC npc);
    public static event NPCAction onNPCItemUsed;

    public NPCData data;

    private enum State
    {
        inQueue, atDesk, Happy, Mad
    }

    [SerializeField] private State state;
    [SerializeField] private NPCVisuals VisualScript;

    //public int positionInQueue;
    private float step = 2f;

    private NPCDialogueData dialogueData;

    private void Start()
    {
        List<NPCDialogueData> allData = GameData.GetAll<NPCDialogueData>();

        foreach (NPCDialogueData dialogue in allData)
        {
            if (dialogue.name == data.ItemNeeded.ToString())
                dialogueData = dialogue;
        }
        
        SetMood(NPCMood.Neutral);
    }

    public void UseItemOnCurrentNPC(UsableItemData itemUsed)
    {
        if (ItemGivenToMe(itemUsed))
        {
            if (data.InteractionParticlePrefab != null)
            {
                GameObject interactionParticle = Instantiate(data.InteractionParticlePrefab, transform.position - transform.forward, Quaternion.identity);
                Destroy(interactionParticle, 2f);
            }
        }
        else
        {
            if (data.BadInteractionParticlePrefab != null)
            {
                GameObject interactionParticle = Instantiate(data.BadInteractionParticlePrefab, transform.position - transform.forward, Quaternion.identity);
                Destroy(interactionParticle, 2f);
            }
        }

        onNPCItemUsed?.Invoke(this);
    }

    public bool ItemGivenToMe(UsableItemData item)
    {
        if (item.ItemType == data.ItemNeeded)
        {
            Debug.Log("GOT ME WHAT I WANT!!!");
            GetHappy();
            return true;
        }
        else
        {
            GetMad();
            return false;
        }
    }

    public void BackToQueue()
    {
        state = State.inQueue;
        SetMood(NPCMood.Neutral);

        if (VisualScript != null)
        {
            VisualScript.SetupVisuals();
        }
    }

    public void AtDesk()
    {
        state = State.atDesk;
        currentNPCAtDesk = this;
        GameManager.Instance.ResetNpcTimer();

        SetMood(NPCMood.Neutral);

        if (dialogueData != null)
            DialogueDrawer.Instance.ShowText(dialogueData.Dialogue.GetRandomElementFromList(), this);
    }

    public void CreateRandomVisuals()
    {
        if (VisualScript != null)
        {
            VisualScript.SetupVisuals();
        }
    }

    private void GetHappy()
    {
        state = State.Happy;
        SetMood(NPCMood.Happy);

        if (SoundEffectManager.instance != null)
            SoundEffectManager.instance.PlaySoundEffectBank("HappyGrunt", 1f);
    }

    private void GetMad()
    {
        state = State.Mad;
        SetMood(NPCMood.Angry);

        if (SoundEffectManager.instance != null)
            SoundEffectManager.instance.PlaySoundEffectBank("AngryGrunt", 1f);
    }

    private void MoveForward()
    {
        transform.position += -transform.forward * step;
    }

    private void SetMood(NPCMood mood)
    {
        if (VisualScript != null)
        {
            VisualScript.SetMood(mood);
        }
    }

}
