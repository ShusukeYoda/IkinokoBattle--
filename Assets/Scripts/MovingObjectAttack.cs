using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovingObjectAttack))]
public class MovingObjectAttack : MonoBehaviour
{
    MovingObjectStatus status;

    // Start is called before the first frame update
    void Start()
    {
        status = GetComponent<MovingObjectStatus>();    
    }
    //攻撃可能であれば攻撃を行う
    public void AttackIfPossible()
    {
        // アタックできるのであれば？
        if (status.IsAttackable) 
        {
            status.GoToAttackStateIfPossible();
        }
    }

    //攻撃対象が攻撃範囲に入ったときに呼ばれる
    public void OnAttackRangeEnter(Collider collider)
    {
        AttackIfPossible();
    }

    [SerializeField]
    AudioSource swingSound;

    //攻撃開始時（コライダーをイネーブル（有効）にする）
    public void OnAttackStart()
    {
        attackCollider.enabled = true;

        if (swingSound != null)
        {
            // スウィング音の再生 pitch(再生速度)をランダムに変化させ、
            // 毎回少し違った音が出るようにしている
            swingSound.pitch = Random.Range(0.7f, 1.3f);
            swingSound.Play();
        }
    }

    [SerializeField]
    Collider attackCollider;

    // attackColliderがヒットしたとき
    public void OnHitAttack(Collider collider)
    {
        // 衝突（攻撃）相手のMovbStatusコンポーネントの取得
        MovingObjectStatus targetMob = collider.GetComponent<MovingObjectStatus>();

        //衝突相手にMobコンポーネントがアタッチされていないときメソッドを抜ける
        if (targetMob == null)
        {
            return;
        }
        {
            //衝突（攻撃）相手のMobStatusのDamageメソッドを呼び出す
            targetMob.Damage(1);
        }
    }

    //攻撃終了時に呼ばれる
    public void OnAttackFinished()
    {
        attackCollider.enabled = false;
        StartCoroutine(CooldownCoroutine());
    }

    //攻撃後に一呼吸おく時間
    [SerializeField]
    float attackCooldownTime = 0.5f;
    IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(attackCooldownTime);
        status.GoToNormalStateIfPossible();
    }
}
