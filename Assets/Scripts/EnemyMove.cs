using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//　P175　先生によるリファクタリング

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyStatus))]     //
public class EnemyMove : MonoBehaviour
{
    NavMeshAgent agent;
    private EnemyStatus status;             //

    [SerializeField]
    LayerMask raycastLayerMask;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        status = GetComponent<EnemyStatus>();//
    }

    public void OnDetectObject(Collider collider)
    {
        // 動ける状態でなければ(!は否定）
        if (!status.IsMovable)
        {
            agent.isStopped = true;
            return;
        }
        if (collider.CompareTag("Player"))
        {
            //プレイヤを目視できれば
            if (CanFindPlayer(collider))
            {
                // 追いかける
                agent.isStopped = false;
                agent.destination = collider.transform.position;
            }
            else
            {
                // 止まる
                agent.isStopped = true;
            }
        }
    }

    bool CanFindPlayer(Collider playerColliider)
    {
        // プレイヤーとエネミーのベクトル (プレイヤーのベクトル ｰ エネミーのベクトル)
        Vector3 potisionDiff = playerColliider.transform.position - transform.position;

        // プレイヤーとエネミーの距離を計算
        float distance = potisionDiff.magnitude;

        // エネミーから見たプレイヤーの方向を計算　(長さ１のベクトル）
        Vector3 direction = potisionDiff.normalized;

        // プレイヤーとエネミーの間の障害物を格納しておく配列
        RaycastHit[] raycastHits = new RaycastHit[10];

        // エネミーとプレイヤー間の障害物の数を計算 (障害物のコライダーの数)
        // RaycastNonAlloc(レイの発射元、方向、衝突したコライダーの配列、距離）
        // エネミーの座標がほぼ地面のため、y軸を0.1だけ上に上げて発射元としています
        int hitCount = Physics.RaycastNonAlloc(transform.position + Vector3.up * 0.1f,
                            direction,
                            raycastHits,
                            distance,
                            raycastLayerMask);

        //Debug.Log(hitCount);

        // 衝突が検知されなければ
        if (hitCount == 0)
        {
            return true;
        }
        // 衝突を１つでも検知した
        else
        {
            return false;
        }
    }
}
/*
教科書コード

public class EnemyMove : MonoBehaviour
{
//    [SerializeField] private PlayerController playerController;

    private NavMeshAgent agent;

    //レイキャストヒット
    private RaycastHit[] raycastHits = new RaycastHit[10];  //

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void OnDetectObject(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            //座標差分と座標間距離
            var positionDiff = collider.transform.position - transform.position;
            var distance = positionDiff.magnitude;
            //プレイヤへの方向 (ヒットしたコライダや座標情報が格納)
            var direction = positionDiff.normalized;
            //RaycastAllとRaycastNonAllocはほぼ同等の機能 
            //こちらだとメモリにゴミが残らないので推奨
            var hitCount = Physics.RaycastNonAlloc(transform.position,
                direction, raycastHits, distance);
            Debug.Log("hitCount: " + hitCount);
            if (hitCount == 0)  //＊＊＊   
            {
                //本作のプレイヤはcharactercontrollerを使っていて、
                //colliderを使っていない、なのでraycastはヒットしない
                //つまり、ヒット数が0であればプレイヤとの間に障害物がないということ
                agent.isStopped = false;                            //
                agent.destination = collider.transform.position;
            }
            else
            {
                //見失ったらストップ
                agent.isStopped = true;
            }
        }
    }

    // Update is called once per frame
//    void Update()
//    {
//        agent.destination = playerController.transform.position;
//    }
}
 */