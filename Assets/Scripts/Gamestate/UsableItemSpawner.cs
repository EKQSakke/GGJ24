using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableItemSpawner : MonoBehaviour
{
    public List<Transform> ItemSpawnPoints;
    public InteractableContainer MyInteractable;

    private List<Interactable> spawnedItems = new List<Interactable>();

    private void Start()
    {
        MyInteractable.ContainerStateChanged += StateChanged;
    }

    public void SetSpawnerInteractableState(bool setTo)
    {
        if (MyInteractable != null)
        {
            MyInteractable.gameObject.SetActive(setTo);
        }
    }
    
    public void CreateItems(List<UsableItemData> possibleItems)
    {
        foreach (Interactable item in spawnedItems)
        {
            Destroy(item.gameObject);
        }

        spawnedItems.Clear();
        List<Transform> spawnPoints = new List<Transform>(ItemSpawnPoints);
        spawnPoints.Shuffle();

        List<UsableItemData> itemList = new List<UsableItemData>(possibleItems);
        itemList.Shuffle();
         
        for (int i = 0; i < spawnPoints.Count && i < possibleItems.Count; i++)
        {
            GameObject newSpawnedItem = Instantiate(itemList[i].Prefab, spawnPoints[i], false);
            spawnedItems.Add(newSpawnedItem.GetComponent<Interactable>());
            possibleItems.Remove(itemList[i]);
        }

        StateChanged();
    }

    private void StateChanged()
    {
        foreach (Interactable item in spawnedItems)
        {
            item.gameObject.SetActive(MyInteractable.ContainerInteractablesAreEnabled);
        }
    }

}
