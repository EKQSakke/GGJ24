using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableItemSpawner : MonoBehaviour
{
    public List<Transform> ItemSpawnPoints;

    private List<GameObject> spawnedItems = new List<GameObject>();
    
    public void CreateItems(List<UsableItemData> possibleItems)
    {
        foreach (GameObject item in spawnedItems)
        {
            Destroy(item);
        }

        spawnedItems.Clear();
        List<Transform> spawnPoints = new List<Transform>(ItemSpawnPoints);
        spawnPoints.Shuffle();

        List<UsableItemData> itemList = new List<UsableItemData>(possibleItems);
        itemList.Shuffle();
         
        for (int i = 0; i < spawnPoints.Count && i < possibleItems.Count; i++)
        {
            GameObject newSpawnedItem = Instantiate(itemList[i].Prefab, spawnPoints[i]);
            spawnedItems.Add(newSpawnedItem);
        }
    }

}
