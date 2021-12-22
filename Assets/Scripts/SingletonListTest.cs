using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonListTest : MonoBehaviour
{
    void Start()
    {
        //OwnedItemData a = OwnedItemData.Instance;
        //例 a.Add(Item.ItemType.Wood,3);

        OwnedItemsData.Instance.Add(Item.ItemType.Wood, 5);
        OwnedItemsData.Instance.Add(Item.ItemType.Stone);
        OwnedItemsData.Instance.Save();

        OwnedItemsData.OwnedItem[] ownedItemList = OwnedItemsData.Instance.OwnedItems;

        foreach (var ownedItem in ownedItemList)
        {
            Debug.Log("アイテムの種類：" + ownedItem.Type + "　　数：" + ownedItem.Number);
        }
    }
}
