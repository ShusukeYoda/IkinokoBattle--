using UnityEngine;

public class ItemDialog : MonoBehaviour
{
    // ボタンの数
    [SerializeField]
    int buttonNumber = 15;

    // アイテムボタンの様々なメンバーが格納されているクラス
    [SerializeField]
    ItemButton itemButton;

    //　複数のアイテムボタンを格納しておくための配列（例えば、Wood(木）、Stone（石）・・・）
    ItemButton[] itemButtons;

    void Start()
    {
        // 最初、このスクリプトがアタッチされているオブジェクト（つまり、ItemDialog）は表示しない
        gameObject.SetActive(false);

        // アイテム欄をbuttonNumberで設定された分だけ表示（インスペクターで変更可能）
        for (int i = 0; i < buttonNumber - 1; i++)
        {
            // 第２引数をtransformにすると、子オブジェクトとして作成されます
            // Grid Layout Gropuコンポーネントにより、自動的に整列されます
            Instantiate(itemButton, transform);
        }

        // ItemButton型のオブジェクトを一気に配列に代入します（自分自身と子オブジェクトも含みます）
        // ヒエラルキーを見るとItemsDialogの子オブジェクトとしてItemButtonが生成されています（上記のInstantiateにより作成）
        itemButtons = GetComponentsInChildren<ItemButton>();
    }

    // アイテム欄の表示、非表示の切り替え（クリックごとに切り替わるようにします）
    public void Toggle()
    {
        // gameObject.activeSelfは、今のアクティブ状態（有効か無効か）を取得します。
        // それの逆（!）なので、このメソッドが呼ばれるたびに反転します）
        gameObject.SetActive(!gameObject.activeSelf);

        // このオブジェクトが有効なら（このスクリプトがアタッチされているオブジェクト。つまりItemDialogオブジェクト）
        if (gameObject.activeSelf)
        {
            // ボタンの数だけ繰り返す
            for (int i = 0; i < buttonNumber; i++)
            {
                //if (OwneditemData.instance.OwnedItems.Length > i)
                itemButtons[i].OwnedItem =
                    
                    OwnedItemsData.Instance.
                    OwnedItems.Length > i ?
                    
                    // trueなら[i] falseならnull
                    OwnedItemsData.Instance.OwnedItems[i] : null;
            }
        }
    }
}
/*
    [SerializeField]
    int buttonNumber = 15;
    [SerializeField]
    // Itembuttonクラス型使用 ＊ゲットコンポーネントしなくていい
    ItemButton itemButton;

    // クラス内でのみ使用
    ItemButton[] itemButtons;

    void Start()
    {
        // 非表示
        gameObject.SetActive(false);

        // アイテム欄を必要な分だけ複製
        for (int i = 0; i < buttonNumber; i++)
        {
            // 第二引数をtransformにすると、子オブジェクトとして作成する
            // GridLayoutGroupコンポーネントにより整列
            Instantiate(itemButton, transform);
        }

        // 子要素のItemButtonを一括取得、配列として保持しておく
        // ItemDialogの子オブジェクトとして
        itemButtons = GetComponentsInChildren<ItemButton>();
    }

    //トグルとは、ある同じ操作を繰り返すことで、
    //機能や状態のON/OFFを切り替える仕組みのこと
    // アイテム欄の表示/非表示を切り替え
    public void Toggle()
    {
        // ゲームオブジェクトが無効ならゲームオブジェクトチェックボックスをオン
        gameObject.SetActive(!gameObject.activeSelf);

        if (gameObject.activeSelf)
        {
            // ボタンの数だけ繰り返す
            for (int i = 0; i < buttonNumber; i++)
            {
                //if (OwneditemData.instance.OwnedItems.Length > i)
                itemButtons[i].OwnedItem = 
                    OwnedItemsData.Instance.
                    OwnedItems.Length > i ? 
                    // trueなら[i] falseならnull
                    OwnedItemsData.Instance.OwnedItems[i] : null;

              /* ifについて
              // アイテムの種類の数（今回は三種）がループ変数以上の場合
              // 今回は、iが2の場合まで成り立つ。（つまり、0と1)
                if (OwnedItemsData.Instance.OwnedItems.Length > i)
                {
                    // 各アイテムボタンに所持アイテム情報をセット
                    itemButtons[i].OwnedItem = OwnedItemsData.
                                               Instance.OwnedItems[i];
                }
                else
                {
                    // ループ中に選択されているアイテム欄を初期化する
                    itemButtons[i].OwnedItem = null;
                }
              */
/*
            }
        }
    }
 */