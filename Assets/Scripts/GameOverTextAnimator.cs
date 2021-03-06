using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverTextAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var transformCache = transform;
        // 終点として使用するため、初期座標を保持
        var defaultPosition = transformCache.localPosition;
        // いったん**の方に移動させる
        transformCache.localPosition = new Vector3(300f, 0);

        //移動アニメーション開始
        transformCache.DOLocalMove(defaultPosition, 1f)
            .SetEase(Ease.OutBounce)
            .OnComplete(() =>
            {
                Debug.Log("GameOver!!");
                //シェイクアニメーション
                transformCache.DOShakePosition(1.5f, 100);
            });

        // DOTweenには、Coroutineを使わずに任意の秒数を持てる便利メソッドも搭載されている
        DOVirtual.DelayedCall(10, () =>
        {
            // 10秒待ってからタイトルシーンに遷移
            SceneManager.LoadScene("TitleScene");
        });
    }
}
