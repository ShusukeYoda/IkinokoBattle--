using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputGetAxisTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Debug.Log($"Axis {h},{v}");
    }
}
/*
        Debug.Log(string.Format("Axisを取得({0},{1})",
            Input.GetAxis("Horizontal"), //横軸情報
            Input.GetAxis("Vertical")    //縦軸情報
            ));
 */
/*未確認
    Debug.Log($"Axisを取得
     {Input.GetAxis("Horizontal")},
     {Input.GetAxis("Vertical")}"
    ));
 */