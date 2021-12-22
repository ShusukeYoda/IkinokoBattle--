using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonSaveTest : MonoBehaviour
{
    void Start()
    {
        OwnedItemsData data1 = OwnedItemsData.Instance;
        data1.hp = 10;

        data1.Save();
    }

}
