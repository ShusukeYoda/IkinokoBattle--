using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[Serializable]
//アタッチするのでモノビヘイビアを残す
public class SingletonTest : MonoBehaviour
{
    void Start()
    {
        OwnedItemsData data1 = OwnedItemsData.Instance;
        data1.hp = 10;

        OwnedItemsData data2 = OwnedItemsData.Instance;
        data2.hp = 20;

        Debug.Log("１つ目のインスタンス：" + data1.hp);
        Debug.Log("2つ目のインスタンス：" + data2.hp);
    }

}

// 出来たら空のオブジェクトを作って張り付けてみる
