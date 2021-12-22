using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1210 : MonoBehaviour
{
    int x = 1;

    void Start()
    {
        x = 3;
    }

    void Update()
    {
        Test();
        Debug.Log(x);
    }

    void Test()
    {
        //int x = 2;
    }
}
