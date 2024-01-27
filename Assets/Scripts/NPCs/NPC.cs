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
    }

    public void AtDesk()
    {
        state = State.atDesk;
        currentNPCAtDesk = this;

        if (dialogueData != null)
            DialogueDrawer.Instance.ShowText(dialogueData.Dialogue.GetRandomElementFromList());
    }

    private void GetHappy()
    {
        state = State.Happy;
    }

    private void GetMad()
    {
        state = State.Mad;
    }

    private void MoveForward()
    {
        transform.position += -transform.forward * step;
    }
}
