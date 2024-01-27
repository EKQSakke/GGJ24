using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public NPCData data;

    private enum State
    {
        inQueue, atDesk, Happy, Mad
    }

    private State state;

    public int positionInQueue;
    private float step = 2f;

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

    // Start is called before the first frame update
    public void AdvanceQueue()
    {
        switch (state)
        {
            case State.inQueue:
                positionInQueue--;
                MoveForward();
                if (positionInQueue == 0)
                {
                    state = State.atDesk;
                    AdvanceQueue();
                }
                break;
            case State.atDesk:
                Debug.Log(data.name + "ServiceInProgress");
                GetHappy();
                break;
            case State.Happy: 
                Debug.Log("Happy");
                Destroy(gameObject);
                break;
            case State.Mad: 
                Debug.Log("Mad");
                Destroy(gameObject);
                break;
        }
    }

    private void GetHappy()
    {
        state = State.Happy;
        AdvanceQueue();
    }

    private void GetMad()
    {
        state = State.Mad;
        AdvanceQueue();
    }

    private void MoveForward()
    {
        transform.position += -transform.forward * step;
    }
}
