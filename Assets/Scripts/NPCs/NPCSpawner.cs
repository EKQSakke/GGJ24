using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField]
    List<NPCData> NPCDatas = new List<NPCData>();

    List<NPC> NPCs = new List<NPC>();

    [SerializeField]
    Vector3 spawnLocation = Vector3.zero;
    [SerializeField]
    float distanceToNextNPC = 2f;
    
    private void Awake()
    {
        PopulateList();
    }
    // Start is called before the first frame update
    private void Start()
    {
        SpawnNPCs();
    }
    void PopulateList()
    {
        string[] assetNames = AssetDatabase.FindAssets("NPC", new[] { "Assets/GameDataObjects/NPC" });
        NPCDatas.Clear();
        foreach (string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var character = AssetDatabase.LoadAssetAtPath<NPCData>(SOpath);
            NPCDatas.Add(character);
        }
    }

    void SpawnNPCs()
    {
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (var item in NPCs)
            {
                if (item != null)
                item.AdvanceQueue();
            }
        }
        
    }
}
