using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStatus : MovingObjectStatus
{
    private NavMeshAgent agent;

    // Start is called before the first frame update
    protected override void Start()
    {
        // 基本クラス（MobStatusクラス）のStart()メソッドを呼び出しています
        base.Start();

        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // agent.velocity.magnitudeでエネミーの速度を計算している
        // 速度が0.01m/sより大きくなったら、walkアニメーションに移行するように
        // Animator Controllerで設定されている
        // 個人的には、EnemyMoveクラスでよいのでは？
        animator.SetFloat("MoveSpeed",agent.velocity.magnitude);
    }

    //animator = GetComponent<NavMeshAgent>();   

    protected override void OnDie()
    {
        // 基本クラス（MobStatusクラス）のOnDie()メソッドを呼び出しています
        base.OnDie();

        StartCoroutine(DestroyCoroutine());
    }

    //3秒後に死ぬ
    IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
