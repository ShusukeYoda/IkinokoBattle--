using System;
using UnityEngine;
//using static UnityEditor.Progress;
using Random = UnityEngine.Random;

// nullを防ぐ
[RequireComponent(typeof(MovingObjectStatus))]
public class MobItemDropper : MonoBehaviour
{
    [SerializeField]
    // アイテムを落とす確率
    // インスペクタに調整バーを表示できる
    [Range(0, 1)]
    // ここの値は初期値のみ有効
    // 10回に1回(仮)
    float dropRate = 0.5f;

    [SerializeField]
    // アイテムのインスタンスを作るためのプレファブをアウトレット接続だが、
    // オブジェクトにアタッチされているコンポーネントでもいい
    Item itemPrefab;

    [SerializeField]
    // 一度に出現するアイテム数
    int number = 1;

    // ドロップした後？
    bool isDropInvoked;
    // 使いたいので、宣言しておく
    MovingObjectStatus status;

    void Start()
    {
        /*例１
        // ゲームオブジェクト型じゃなくてもインスタンシエイトできる
        // Item型だと分かっているから
        　　//Item itemScript = itemPrefab.GetComponent<Item>();
        Item itemScript = itemPrefab;
        Instantiate(itemPrefab);
        //Instantiate<Item>(itemPrefab);
        */

        // RequireComponenしているので、nullになることはない
        status = GetComponent<MovingObjectStatus>();
    }

    void Update()
    {
        // エネミーのライフが0以下ならば
        if (status.Life <= 0)
        {
            // もしドロップする必要があるなら
            DropIfNeeded();
        }
    }

    private void DropIfNeeded()
    {
        if (isDropInvoked)
        {
            return;
        }

        isDropInvoked = true;

        // ２回目以降が呼ばれた場合は以下のコードは実行されない

        // floatの場合は1fと書いたら1を含む
        if (Random.Range(0, 1f) >= dropRate)
        {
            return;
        }

        for (int i = 0; i < number; i++)
        {
            // インスタンスを作るオブジェクト（または、それが持っているコンポーネント）
            // 発生させる場所
            // 回転
            Item item = Instantiate<Item>(itemPrefab,
                                          transform.position,
                                          Quaternion.identity);
            // 作ったアイテムが持っているスクリプトの初期化メソッドを呼び出す
            item.Initialize();
        }
    }
}
//DropIfNeeded();