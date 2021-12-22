using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// ファイルに保存するため、シリアライズ化する
// UnityのUpdateメソッド等を使わないため、MonoBehaviourは継承しない
[Serializable]
public class OwnedItemsData
{
    #region シングルトンのブロック

    // プライベートの文字列の値として設定している（直接コードに記述しないようにした見やすくする
    const string PlayerPrefsKey = "OWNED_ITEM_DATA";

    // コンストラクタですが、プライベートのアクセス修飾子がついている。
    // これによって、new OwnedItemData()が実行できず、インスタンスを作ることができない
    private OwnedItemsData()
    {

    }

    // このクラスを型とするフィールドを宣言
    // 自分の型を型名とすることもできる
    // 初期値が代入されていないときは、デフォルト値として、nullが代入されている
    static OwnedItemsData instance;

    // 上記のプライベートフィールドのinstanceを公開するためのプロパティ
    // 戻り値の型はこのクラス（型）になる
    public static OwnedItemsData Instance
    {
        // このプロパティが取得された時に呼ばれるブロック
        get
        {
            // 初期の場合（１回目に呼ばれたとき）
            if (instance == null)
            {
                
                // もし、すでにPlayerPrefsKey（文字列）の保存データが存在していたら（持っていたら）
                if (PlayerPrefs.HasKey(PlayerPrefsKey))
                {
                    // インスタンスとして、取り出したデータを代入する（型は自身のクラス（OwnedItemData))
                    instance = JsonUtility.FromJson<OwnedItemsData>(PlayerPrefs.GetString(PlayerPrefsKey));
                }
                // 保存されたデータがなければ
                else
                {
                    // 新しくインスタンスを作る
                    instance = new OwnedItemsData();
                }
                
            }

            // いずれかで代入されたインスタンスを戻り値として返す
            return instance;
        }
    }

    // テスト用に作成
    public int hp;

    #endregion

    #region セーブメソッド

    // インスタンスをJson形式にシリアライズして文字列型として保存する
    public void Save()
    {
        // このインスタンス(this)をJsonに変換して、代入
        // 教科書とは違うが、ToJsonメソッドの第２引数は、人に見やすいフォーマット（改行あり）にするスイッチになっています
        string jsonString = JsonUtility.ToJson(this, true);

        // 確認用コード
        Debug.Log(jsonString);

        // ファイルに文字列として、PlayerPrefsKeyキーで書き込む
        PlayerPrefs.SetString(PlayerPrefsKey, jsonString);

        // 保存処理（この時点でメディアに書き込む（HDDやSSD)
        // 実際の保存場所は、プラットフォームによって違う。次を参照
        //　https://docs.unity3d.com/ja/2018.4/ScriptReference/PlayerPrefs.html
        PlayerPrefs.Save();
    }

    #endregion

    #region 持ち物の基本の情報が入っているクラスを作成

    // 自分のアイテムのインナークラス（クラスの中にクラス）
    // このクラスのリストが欲しいので、作っておく
    // UnityのJsonUtilityで扱える型は決まっています。（プロパティはダメ）
    // 参考　https://qiita.com/keidroid/items/24e03f82d74560dc557a
    [Serializable]
    public class OwnedItem
    {
        // 持ってるアイテムの種類（プライベートメンバー））
        [SerializeField]
        Item.ItemType type;

        // 持ってるアイテムの種類（外部からのアクセス用プロパティ。外部からは、取得だけ）
        public Item.ItemType Type
        {
            get
            {
                return type;
            }
        }

        // 持っているアイテムの数（プライベートメンバー）
        [SerializeField]
        int number;

        // 持っているアイテムの数（外部からのアクセス用プロパティ。外部からは、取得だけ）
        public int Number
        {
            get
            {
                return number;
            }
        }

        // コンストラクタ（newで作る時の引数はアイテムの種類）
        public OwnedItem(Item.ItemType type)
        {
            this.type = type;
        }

        // アイテムを追加する
        // このメソッドを呼び出す時の引数は、いくつアイテムを足すかになる。（引数を省略すると1が代入される）
        public void Add(int number = 1)
        {
            this.number += number;
        }

        // アイテムを使う
        // このメソッドを呼び出す時の引数は、いくつアイテムを足すかになる。（引数を省略すると1が代入される）
        public void Use(int number = 1)
        {
            this.number -= number;
        }
    }

    #endregion

    #region 持ち物のリストを作成（リストの内容は、持ち物の内容は、インナークラスのOwnedItem型

    // OwnedItem型のインスタンスが格納されるリスト
    //　持ち物の種類（ItemTypeごと）がリストの要素にする。今回は３種類なので、リストの要素数は３になる
    [SerializeField]
    List<OwnedItem> ownedItems = new List<OwnedItem>();

    // アイテム一覧を取得するメソッド（外部から呼ばれる）
    // ポイントとして、内部の型がListで、呼び出すときは配列とする（したい？）ため、ToArray()メソッドで変換している
    //これは、メソッドにして、名前をGetOwnedItemsのほうが望ましい・・・//インテリセンスで変更可能
    public OwnedItem[] OwnedItems
    {
        get
        {
            return ownedItems.ToArray();
        }
    }

    // リストの中から指定したアイテムの種類のインスタンスを取り出す
    public OwnedItem GetItem(Item.ItemType type)
    {
        // LINQを使って、リストの中からタイプ（アイテムの種類）が一致すれば、その要素を返す
        return ownedItems.FirstOrDefault(ownedItem => ownedItem.Type == type);
    }

    // リストに足すメソッド
    public void Add(Item.ItemType type, int number = 1)
    {
        // アイテムのリストからアイテムの種類が一致する要素を探して取得する
        OwnedItem item = GetItem(type);

        // 一致しなければ（まだ、そのアイテムが登録されていなければ）、作って、追加しておく
        if (item == null)
        {
            item = new OwnedItem(type);
            ownedItems.Add(item);
            Debug.Log("NewAdd");

        }

        // その要素（その種類のアイテム）の数に指定数分追加する
        // このAddメソッドは、インナークラスのAddメソッドです
        item.Add(number);
        Debug.Log("Add");
    }

    // アイテムを使う
    // アイテムリストから該当するアイテムの種類の要素のNumberプロパティから減らす
    // ただし、足りなければ、エラーの例外処理を投げる
    public void Use(Item.ItemType type, int number = 1)
    {
        // アイテムのリストからアイテムの種類が一致する要素を探して取得する
        OwnedItem item = GetItem(type);

        // アイテムの種類が持っていないアイテム（登録されてない）の場合、エラーとする（無い種類から減らすことができないため）
        // または、持っているアイテムの数が減らそうとしている数より少ない時、エラーとする（持っている数以上は減らせないです）
        if (item == null || item.Number < number)
        {
            throw new Exception("アイテムが足りません");
        }

        // その要素（その種類のアイテム）の数から指定数分減らす
        // このUseメソッドは、インナークラスのUseメソッドです
        item.Use(number);
    }

    #endregion

}
