using UnityEngine;
using UnityEngine.UI;

// ライフゲージプレファブにアタッチするスクリプト
public class LifeGauge : MonoBehaviour
{
    // ライフ満タンの画像
    [SerializeField]
    Image fillImage;

    // レクトトランスフォーム(コンポーネント)を取得する
    // ライフゲージは、親オブジェクト（ＵＩ）上に表示するため
    RectTransform parentRectTransform;

    // ワールド座標からスクリーン座標　用
    Camera _camera;

    // ライフのパーセント計算とワールド座標を必要とするメソッド　用
    // 動いているオブジェクトが共有して持っている
    MovingObjectStatus status;

    // ゲージを更新
    void Update()
    {
        Refresh();
    }
    // 初期化
    public void Initialized(RectTransform parentRectTransform,
        Camera camera, MovingObjectStatus status)
    {
        // それぞれのプライベートフィールドに値を代入
        this.parentRectTransform = parentRectTransform;
        _camera = camera;
        this.status = status;

        // 自分のゲージの更新作業
        Refresh();
    }

    // ゲージの更新
    void Refresh()
    {
        // 現在のライフの割合で表示の長さを決めます（0-1の間）
        fillImage.fillAmount = status.Life / status.LifeMax;

        // 対象Nobの場所にゲージを移動。World座標やLocal座標を変換するときはRectTransformUtilityを使う
        // statusはゲームオブジェクトではありませんが、この書き方で、アタッチしているオブジェクトの位置を取得できます。
        // このメソッドで、3D座標から2Dスクリーン座標に変換できます。
        //  _camera.は、Camera.main.と置き換えでもOK。これは、Cameraというタグがついているカメラを取得することをさします。
        Vector3 screenPoint = _camera.WorldToScreenPoint(status.transform.position);

        // 上記screenPoint（2Dスクリーン座標）を親オブジェクトのRectTransformの座標に変換して、localPointに代入
        // 今回は、CanvasのRender ModeがScreen Space が　Overlayなので、第3引数にnullを指定している。
        // Screen Space が　Cameraの場合は、対象のカメラを渡す必要がある
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, screenPoint, null, out Vector2 localPoint);

        // ゲージがキャラに重なるので少し上にずらしている
        transform.localPosition = localPoint + new Vector2(0, 80);
    }
}
/*
    // ライフ満タンの画像
    [SerializeField]
    Image fillImage;

    // レクトトランスフォームを取得する(コンポーネント)
    // ライフゲージは、親オブジェクト（ＵＩ）上に表示するため
    RectTransform parentRectTransform;

    // ワールド座標からスクリーン座標　用
    Camera _camera;

    // ライフのパーセント計算とワールド座標を必要とするメソッド　用
    // 動いているオブジェクトが共有して持っている
    MovingObjectStatus status;

    void Update()
    {
        Refresh();
    }

    // 初期化
    public void Initialized(RectTransform parentRectTransform,
        Camera camera,MovingObjectStatus status)
    {
        // それぞれのプライベートフィールドに値を代入
        this.parentRectTransform = parentRectTransform;
        _camera = camera;
        this.status = status;

        // 自分のゲージの更新作業
        Refresh();
    }

    private void Refresh()
    {
        // 残りライフ
        fillImage.fillAmount = status.Life / status.LifeMax;
    }

    // ゲージの更新
    void Refresh()
    {
        // 現在のライフの割合で表示の長さを決めます（0-1の間）
        fillImage.fillAmount = status.Life / status.LifeMax;

        // 対象Nobの場所にゲージを移動。World座標やLocal座標を変換するときはRectTransformUtilityを使う
        // statusはゲームオブジェクトではありませんが、この書き方で、アタッチしているオブジェクトの位置を取得できます。
        // このメソッドで、3D座標から2Dスクリーン座標に変換できます。
        //  _camera.は、Camera.main.と置き換えでもOK。これは、Cameraというタグがついているカメラを取得することをさします。
        Vector3 screenPoint = _camera.WorldToScreenPoint(status.transform.position);

        // 上記screenPoint（2Dスクリーン座標）を親オブジェクトのRectTransformの座標に変換して、localPointに代入
        // 今回は、CanvasのRender ModeがScreen Space が　Overlayなので、第3引数にnullを指定している。
        // Screen Space が　Cameraの場合は、対象のカメラを渡す必要がある
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, screenPoint, null, out Vector2 localPoint);

        // ゲージがキャラに重なるので少し上にずらしている
        transform.localPosition = localPoint + new Vector2(0, 80);
    }
 */
/*
    // ライフが満タンのイメージ
    [SerializeField]
    Image fillImage;

    // 親のRectTransform（UIのTransform）
    // ライフゲージは、親オブジェクト（UI）上に表示するため
    RectTransform parentRectTransform;

    // 3Dのオブジェクト用座標(ワールド座標）からスクリーン座標（左上を0,0地点とした画面の座標）に変換するための必要
    Camera _camera;

    // 動いているオブジェクトが共通で持っているスクリプト
    // ライフゲージのパーセント計算とワールド座標を必要とするメソッドでオブジェクトの座標の引数として使います。
    // コンポーネント.transform.positionは、gameObject.transform.positionと同様の座標がえられます。
    // ただし、コンポーネントは、アタッチしているオブジェクトであること
    MovingObjectStatus status;

    // このスクリプトをアタッチしているプレファブを外部で作った時（Instantiate）にすぐ呼ぶための初期値代入メソッド
    // C#での引数ありのコンストラクタと同様なことを実現したいため
    public void Initialize(RectTransform parentRectTransform, Camera camera, MovingObjectStatus status)
    {
        // それぞれのプライベートフィールドに値を代入しています。
        this.parentRectTransform = parentRectTransform;
        _camera = camera;
        this.status = status;

        // 自分のゲージの更新作業
        Refresh();
    }

    // 自分でゲージを更新しています。
    void Update()
    {
        Refresh();
    }

    // ゲージの更新
    void Refresh()
    {
        // 現在のライフの割合で表示の長さを決めます（0-1の間）
        fillImage.fillAmount = status.Life / status.LifeMax;

        // 対象Nobの場所にゲージを移動。World座標やLocal座標を変換するときはRectTransformUtilityを使う
        // statusはゲームオブジェクトではありませんが、この書き方で、アタッチしているオブジェクトの位置を取得できます。
        // このメソッドで、3D座標から2Dスクリーン座標に変換できます。
        //  _camera.は、Camera.main.と置き換えでもOK。これは、Cameraというタグがついているカメラを取得することをさします。
        Vector3 screenPoint = _camera.WorldToScreenPoint(status.transform.position);

        // 上記screenPoint（2Dスクリーン座標）を親オブジェクトのRectTransformの座標に変換して、localPointに代入
        // 今回は、CanvasのRender ModeがScreen Space が　Overlayなので、第3引数にnullを指定している。
        // Screen Space が　Cameraの場合は、対象のカメラを渡す必要がある
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, screenPoint, null, out Vector2 localPoint);

        // ゲージがキャラに重なるので少し上にずらしている
        transform.localPosition = localPoint + new Vector2(0, 80);
    }
 */