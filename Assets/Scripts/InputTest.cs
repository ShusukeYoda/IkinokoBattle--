using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //タッチ操作対応か否か //＊タッチ対応していない＊
        /*
        Debug.Log(Input.touchSupported ?
            "タッチに対応しています" : "タッチに対応していません");
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift)&&Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Shift+Spaceを押しました！");
        }
        /*
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("左クリックを押しました");
        }
        */
        //マウス座標を表示
        //Debug.Log("マウス座標: " + Input.mousePosition);

        //ホイールスクロール量を表示 //-2から+2くらい
        Debug.Log("マウスホイールのスクロール量: " + Input.mouseScrollDelta);

        //タッチ数を取得する //*タッチ非対応＊
        //Debug.Log(string.Format("現在のタッチ数は{0}です", Input.touchCount));
    }
}
