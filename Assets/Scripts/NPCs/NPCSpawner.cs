using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] GameObject interactionParticlePrefab;
    [SerializeField]
    List<NPCData> NPCDatas = new List<NPCData>();

    List<NPC> NPCs = new List<NPC>();

    [SerializeField]
    Vector3 spawnLocation = Vector3.zero;
    [SerializeField]
    float distanceToNextNPC = 2f;

    // Start is called before the first frame update
    private void Start()
    {      
        if (GameManager.Instance == null)
            SpawnNPCs();
    }

    public void SpawnNPCs()
    {
        if (NPCDatas.IsEmpty())
            NPCDatas = GameData.GetAll<NPCData>();

        ClearQueue();
        int positionInQueue = 1;

        foreach (NPCData data in NPCDatas)
        {
            NPC nPC = Instantiate(data.NPCPrefab, spawnLocation, Quaternion.identity).GetComponent<NPC>();
            spawnLocation = new Vector3(spawnLocation.x, spawnLocation.y, spawnLocation.z + distanceToNextNPC);
            NPCs.Add(nPC);
            nPC.positionInQueue = positionInQueue;
            positionInQueue++;
            nPC.data = data;
        }
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

    public void UseItemOnCurrentNPC(UsableItemData itemUsed)
    {
        if (NPCs.IsEmpty())
            return;

        Debug.Log("im here");
        AdvanceNextNPC();
    }

    private void AdvanceNextNPC()
    {
        if (NPCs.IsEmpty())
            return;

        foreach (var item in NPCs)
        {
            item.AdvanceQueue();
        }

        if (interactionParticlePrefab != null)
        {
            GameObject interactionParticle = Instantiate(interactionParticlePrefab, NPCs[0].transform.position, Quaternion.identity);
            Destroy(interactionParticle, 2f);
        }
        
        NPCs.RemoveAt(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AdvanceNextNPC();
        }        
    }
}
