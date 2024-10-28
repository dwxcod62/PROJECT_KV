using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    public GameObject droppedItemPrefab;
    public List<ItemController> LootListGameObj = new List<ItemController>();

    ItemController GetDroppedItem()
    {
        int randomNumber = Random.Range(1, 101);
        List<ItemController> possibleItems = new List<ItemController>();

        foreach (ItemController item in LootListGameObj)
        {
            if (randomNumber <= item.dropChance)
            {
                possibleItems.Add(item);
            }
        }
        if (possibleItems.Count > 0)
        {
            ItemController droppedItem = possibleItems[Random.Range(0, possibleItems.Count)];
            return droppedItem;
        }
        return null;
    }

    public void InstantiateLoot(Vector3 spawnPosition)
    {
        ItemController droppedItem = GetDroppedItem();
        if (droppedItem != null)
        {
            GameObject lootGameObject = Instantiate(droppedItem.gameObject, spawnPosition, Quaternion.identity);
        }
    }

}
