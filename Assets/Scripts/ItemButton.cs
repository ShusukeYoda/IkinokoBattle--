using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ItemButton : MonoBehaviour
{
    #region アイテムのボタンに表示するためのメンバーが定義されてるクラス(インナークラス）
    [Serializable]
    public class ItemTypeSpriteMap
    {
        // アイテムの種類
        public Item.ItemType itemType;

        // アイテムの画像
        public Sprite sprite;
    }

    #endregion

    // 上記のメンバーを代入する配列
    [SerializeField]
    ItemTypeSpriteMap[] itemSprites;

    // アイテムのイメージをインスペクターで登録するフィールドを宣言しておく
    [SerializeField]
    Image image;

    // 持っているアイテムの個数をインスペクターで登録するフィールドを宣言しておく
    [SerializeField]
    Text number;

    //　コンポーネントのボタン（アイテムボタン）の参照を取得するためのフィールド
    Button button;

    // 自分の持っているアイテムの情報を格納しているクラスの参照のためのフィールド
    OwnedItemsData.OwnedItem ownedItem;

    // 自分の持っているアイテムへのアクセス用プロパティ
    public OwnedItemsData.OwnedItem OwnedItem
    {
        get
        {
            return ownedItem;
        }
        set
        {
            ownedItem = value;

            // アイテムが割り当てられたかどうかでアイテム画像や所持個数の表示を切り替える
            // owunedItemが空ならtrueが代入される
            bool isEmpty = ownedItem == null;

            // 持っているアイテムが空でなければ、イメージオブジェクトを有効（表示される）
            image.gameObject.SetActive(!isEmpty);

            // 持っているアイテムが空でなければ、所持数オブジェクトを有効（表示される）
            number.gameObject.SetActive(!isEmpty);

            // 持っているアイテムが空でなければ、ボタン機能（そのオブジェクト）が有効
            button.interactable = !isEmpty;

            // 持っているアイテムが空でなければ
            if (!isEmpty)
            {
                // イメージオブジェクトには、最初に合致した（アイテムの種類が）スプライトが代入される
                image.sprite = itemSprites.First(type => type.itemType == ownedItem.Type).sprite;

                // 所持数オブジェクトには、所持数が代入される
                // ""は文字なしだが、後のownedItem.Numberがint型なので、文字列+整数は文字列型になる条件を使っている
                // ownedItem.Number.ToString()のほうがいいでしょう
                number.text = "" + ownedItem.Number;
            }
        }
    }

    // Startメソッドよりも先に実行されます
    void Awake()
    {
        // このスプリクトと同じところにあるButtonコンポーネントの取得
        button = GetComponent<Button>();

        // このボタンをクリックした時のイベントハンドラを登録
        // ボタンが押された時に実行されるメソッドの登録です
        button.onClick.AddListener(onClick);
    }

    void onClick()
    {
        //TODO アイテムを押した時（アイテムが選択された時）に実行されるコード
    }
}

/*
    [Serializable]
    public class ItemTypeSpriteMap
    {
        //『itemTypeのときのsprite』用
        // 例：woodのときのｽﾌﾟﾗｲﾄ
        public Item.ItemType itemType;
        public Sprite sprite;
    }

    [SerializeField]
    // 上述したクラス型
    ItemTypeSpriteMap[] itemSprites;
    [SerializeField]
    Image image;
    [SerializeField]
    Text number;
    // コンポーネントのボタンの参照を取得
    Button button;
    // 自分の持っているアイテムの情報を格納しているクラス内クラス型
    OwnedItemsData.OwnedItem ownedItem;

    // プロパティ状：自分の持ってるアイテムへのアクセス
    public OwnedItemsData.OwnedItem OwnedItem
    {
        get
        {
            return ownedItem;
        }
        set
        {
            ownedItem = value;

            // 空ですか？　nullならtrue
            bool isEmpty = ownedItem == null;

            // 持っているアイテムが空でなければ
            // イメージのオブジェクトのチェックボックスをオン
            image.gameObject.SetActive(!isEmpty);
            // 所持数のオブジェクトのチェックボックスをオン
            number.gameObject.SetActive(!isEmpty);
            // ボタン機能を押せる
            button.interactable = !isEmpty;

            if (!isEmpty)
            {
                // 最初に合致したアイテムの種類がｽﾌﾟﾗｲﾄに代入する
                //『itemTypeのときのsprite』
                image.sprite = itemSprites.First(type => type.itemType == ownedItem.Type).sprite;

                // 所持数オブジェクトには、所持数が代入される
                // ""は文字なしだが、後のownedItem.Numberがint型なので、
                // 文字列+整数は文字列型になる条件を使っている
                number.text = "" + ownedItem.Number;
            }
        }
    }
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(onClick);
    }
    void onClick()
    {
        // ToDo
    }
 */