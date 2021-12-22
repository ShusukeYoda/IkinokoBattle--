using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    [SerializeField] 
    GameObject enemyPrefab;
    [SerializeField]
    PlayerStatus playerStatus;

    // Start is called before the first frame update
    void Start()
    {
        // コルーチンを実行する
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        // 10秒おきに処理を無限に繰り返す
        while (true)
        {
            //距離10のベクトル
            var distanceVector = new Vector3(10, 0);


            //プレイヤの位置をベースにした敵の出現位置
            //**Y軸に対して上記ベクトルをランダムに0°～360°回転させている**
            var spawnPositionFromPlayer = Quaternion.Euler(0, Random.Range(0, 360f), 0) * distanceVector;


            //出現させたい位置を決定
            var spawnPosition = playerStatus.transform.position + spawnPositionFromPlayer;

            //指定座標から一番近いNavMeshの座標を探す
            NavMeshHit navMeshHit;
            if (NavMesh.SamplePosition(spawnPosition, out navMeshHit, 10, NavMesh.AllAreas))
            {
                //処理A
                Instantiate(enemyPrefab, navMeshHit.position, Quaternion.identity);
            }

            // 10秒処理を待つ
            yield return new WaitForSeconds(10);

            //処理B
            if (playerStatus.Life <= 0)
            {
                break;
            }
        }
    }
}


/*
ひな形
public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // コルーチンを実行する
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        // 10秒おきに処理を無限に繰り返す
        while (true)
        {
            //　x軸方向に長さ10のベクトルを作成します
            Vector3 distanceVector = new Vector3(10, 0, 0);
            
            //　y軸を中心として、上記ベクトルを90°右回転させる
            Vector3 spawnPositionFromPlayer =  Quaternion.Euler(0, 90, 0) * distanceVector;
         
            // クエリちゃんの位置　+　作成したベクトルを計算
            Vector3 spawnPosition = playerStatus.transform.position + spawnPositionFormPlayer;

            // NavMeshの発見されたエリア（ポイント）を代入する変数を用意
            NavMeshHit navmeshHit;

            // 特定の位置で一番近いNavMeshの発見されたエリア（ポイント）を取得する
            //　NavMesh.SamplePosition(特定の位置、ポイントを代入する変数、最大調べる範囲、全てのエリア
            //　戻り値は、見つけられたかどうか？(bool型）
            if(NavMesh.SamplePosition(spawnPosition, out navMeshHit, 10,NavMesh.AllAreas))
            {
            //処理A
            Instantiate(enemyPrefab);
            }

            // 10秒処理を待つ
            yield return new WaitForSeconds(10);

            //処理B
            if (playerStatus.Life <= 0)
            {
                break;
            }
        }
    }
}
 */

/*
    IEnumerator SpawnLoop()
    {
        // 10秒おきに処理を無限に繰り返す
        while (true)
        {
            //距離10のベクトル
            var distanceVector = new Vector3(10, 0);


            //プレイヤの位置をベースにした敵の出現位置
            //**Y軸に対して上記ベクトルをランダムに0°～360°回転させている**
            var spawnPositionFromPlayer = Quaternion.Euler(0, Random.Range(0, 360f), 0) * distanceVector;


            //出現させたい位置を決定
            var spawnPosition = playerStatus.transform.position + spawnPositionFromPlayer;

            //指定座標から一番近いNavMeshの座標を探す
            NavMeshHit navMeshHit;
            if (NavMesh.SamplePosition(spawnPosition, out navMeshHit, 10,NavMesh.AllAreas))
            {
                 //処理A
                 Instantiate(enemyPrefab, navMeshHit.position, Quaternion.identity);
            }

            // 10秒処理を待つ
            yield return new WaitForSeconds(10);

            //処理B
            if (playerStatus.Life <= 0)
            {
                break;
            }
        }
    }

 */