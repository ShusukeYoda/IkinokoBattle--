using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MovingObjectStatus
{
    protected override void OnDie()
    {
        base.OnDie();
        // プレイヤが倒れたときのゲームオーバー処理
        StartCoroutine(GoToGameOverCoroutine());
    }

    private IEnumerator GoToGameOverCoroutine()
    {
        // 3秒待ってからゲームオーバーシーンへ遷移
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("GameOverScene");
    }
}
