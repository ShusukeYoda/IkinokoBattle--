using DG.Tweening;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Wood,
        Stone,
        ThrowAxe
    }

    // インスペクタで表示される
    [SerializeField]    
    ItemType type;

    // 初期化処理：イニシャライズ
    public void Initialize()
    {
        // アニメが終わるまでcolliderを無効に
        Collider colliderCache = GetComponent<Collider>();
        // イネーブルチェックボックスを外す
        // コリジョンを無効にすることで、当たり判定を無効にして
        // 検出できないようにする まだアイテムをゲットできない
        colliderCache.enabled = false;


        // 出現アニメーション

        //トランスフォームキャッシュ
        Transform transformCache = GetComponent<Transform>();
        // ドロップポジション：ｘとｚをランダムレンジ
        // 敵がアイテムを落とす場所は、今の敵の場所＋ランダムの座標を足したところ
        Vector3 dropPosition = transform.localPosition +
            new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));

        // 0.5秒かけて今の位置から移動させる描画
        transformCache.DOLocalMove(dropPosition, 0.5f);

        // 今のスケール値を一旦記憶しておく
        Vector3 defaultScale = transformCache.localScale;
        // 今のスケール値を0にする
        transformCache.localScale = Vector3.zero;

        // 0.5秒かけて今のスケールからdefaultに戻す描画
        transformCache.DOScale(defaultScale, 0.5f)
            //始点と終点のつなぎ方の設定(今回はスケール)
            .SetEase(Ease.OutBounce)
            //完了時匿名メソッド使用(引数の型がデリゲート型:引数にメソッド)
            .OnComplete(() =>
            {
                // イネーブルチェックボックスを付ける
                // コリジョンを有効にする アイテムをゲットできる
                colliderCache.enabled = true;
            });
    }

    // 触れた最初
    void OnTriggerEnter(Collider other)
    {
        // トリガーがかかった相手のタグがプレイヤではないとき
        if (!other.CompareTag("Player"))
        {
            return;
        }

        // TODO プレイヤの所持品として追加
        OwnedItemsData.Instance.Add(type);
        OwnedItemsData.Instance.Save();

        //所持アイテムのログ出力
        foreach (var item in OwnedItemsData.Instance.OwnedItems)
        {
            Debug.Log($"{item.Type}を{item.Number}個所持");
        }

        // このスクリプトがアタッチされているオブジェクトを破棄
        Destroy(gameObject);
    }
}
