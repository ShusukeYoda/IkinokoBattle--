using System;
using System.Collections.Generic;
using UnityEngine;

// 各オブジェクトのライフゲージを入れておく置き場所
// ヒエラルキーでは、このスクリプトがアタッチされているオブジェクトの
// 子オブジェクトとして各オブジェクトのライフゲージが追加されていきます。
[RequireComponent(typeof(RectTransform))]
public class LifeGaugeContainer : MonoBehaviour
{
    // ライフゲージ表示対象のMobを写しているカメラ
    [SerializeField]
    Camera mainCamera;

    // ライフゲージのプレファブ（このオブジェクトには、LifeGageスクリプトがアタッチされています）
    [SerializeField]
    LifeGauge lifeGaugePrefab;

    // このスクリプトがアタッチされているオブジェクトはCanvasになります。
    // 座標は、UI座標になります
    RectTransform rectTransform;

    #region シングルトン（このインスタンスが複数作られないように）
    // シーン上に１つしか存在させないスクリプトのため、このような疑似シングルトンが成り立つ

    // このスクリプトを型とするスタティックフィールドの宣言
    // 初期値の代入を省略すると、デフォルトとしてnullが代入されます
    static LifeGaugeContainer instance;

    // 他のスクリプトから呼ばれたら・・・
    // スタティックメソッドなので、呼び出し方は、（クラス名.プロパティ名）となる
    // LifeGaugeContainer.Instanceが具体的な呼び出し方
    // 戻り値は、このスクリプトのインスタンスなので、下のAddメソッドや
    // Removeメソッドを呼び出したい場合は、これに続けて記述する
    // LifeGaugeContainer.Instance.Add(引数）が具体的な呼び出し方
    public static LifeGaugeContainer Instance
    {
        // Instanceと書き間違えると無限ループになる
        get { return instance; }
    }

    // このスクリプトがインスタンス化された時に最初に実行されるメソッド
    // 上記のプロパティの呼び出しは、他のスクリプトからのため、こちらの方が先に実行される
    private void Awake()
    {
        // !=で　「nullではない」と読みます
        // 初期値がnullなので、Awake時にすでに何か代入されている場合・・・となります。
        if (null != instance)
            // Awakeの時（作成された時）に既に何か代入されているとは考えられないため、例外エラーとしています）
            throw new Exception(
        "LifeBarContainer instance already exists.");
        // 初期の状態の時（繰り返しですがAwakeは一回にか実行されません）に、このスクリプトのインスタンスを代入しておきます。
        // これでinstanceはnullにならない
        instance = this;
        // 負荷を減らすためにコンポーネントをキャッシュ（一時保存）しておく。
        // 実際に使うときは、こちらを使う
        rectTransform = GetComponent<RectTransform>();
    }
    #endregion

    // モバイルオブジェクト共通ステータスのスクリプトをキーに、
    // ライフゲージを値とするディクショナリ型フィールドを宣言して初期化
    readonly Dictionary<MovingObjectStatus, LifeGauge>
        statusLifeBarMap = new Dictionary<MovingObjectStatus, LifeGauge>();

    // 宣言したディクショナリに要素と追加する
    // このメソッドを呼び出すときは、引数にモバイルオブジェクト共通ステータスの
    // スクリプト（インスタンス）を渡す。
    // 呼び出しもとは、MobStatusのStartメソッド。オブジェクトがインスタンスされた時に
    // アタッチされているMobStatusのStartメソッドが実行される
    public void Add(MovingObjectStatus status)
    {
        // ここでライフゲージのオブジェクトを作成している。作成場所は、
        // ライフゲージコンテナの子。第２引数をtransformとすることで実現される。
        LifeGauge lifeGauge = Instantiate(lifeGaugePrefab, transform);
        // 作ったライフゲージのInitialize（初期化）メソッドを呼び出す。
        // （C#での引数付きコンストラクタのような働き。作成時にそのインスタンスに情報を渡している）
        lifeGauge.Initialized(rectTransform, mainCamera, status);
        statusLifeBarMap.Add(status, lifeGauge);
    }
    // 宣言してディクショナリから要素を削除する
    // MobStatusスクリプトのOnDieメソッドから呼ばれる（死んだ時に実行されるメソッド）
    public void Remove(MovingObjectStatus status)
    {
        // 引数で与えられたMobStatusスクリプトがアタッチされているオブジェクトをデストロイする
        Destroy(statusLifeBarMap[status].gameObject);
        // ディクショナリから削除（管理一覧から削除）
        statusLifeBarMap.Remove(status);
    }
}
