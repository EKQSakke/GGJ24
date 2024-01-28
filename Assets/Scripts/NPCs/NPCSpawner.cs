using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;

public class NPCSpawner : MonoBehaviour
{
    public delegate void NPCSpawnerAction(List<NPC> npcs);
    public static event NPCSpawnerAction onSpawned;

    [SerializeField] private NPCMover mover;
    [SerializeField] List<NPCData> NPCDatas = new List<NPCData>();

    List<NPC> NPCs = new List<NPC>();

    private void Start()
    {      
        if (GameManager.Instance == null)
            SpawnNPCs(mover.AmountOfQueuePoints);
    }

    public void SpawnNPCs(int amount, List<NPCData> allowedNPCs = null)
    {
        if (allowedNPCs != null)
            NPCDatas = allowedNPCs;
        else if (NPCDatas.IsEmpty())
            NPCDatas = GameData.GetAll<NPCData>();

        ClearQueue();

        for (int positionInQueue = 0; positionInQueue < amount; positionInQueue++)
        {
            NPCData data = NPCDatas.GetRandomElementFromList();
            NPC nPC = Instantiate(data.NPCPrefab).GetComponent<NPC>();
            nPC.data = data;
            nPC.CreateRandomVisuals();
            NPCs.Add(nPC);            

            if (positionInQueue == amount - 1)
                nPC.AtDesk();
        }

        onSpawned.Invoke(NPCs);
    }

    public void ClearQueue()
    {
        foreach (var item in NPCs)
        {
            if (item != null)
                Destroy(item.gameObject);
        }

        NPCs.Clear();
    }
}
