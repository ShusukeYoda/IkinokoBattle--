using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 抽象クラス
public abstract class MovingObjectStatus : MonoBehaviour
{
    // 列挙型
    protected enum StateEnum
    {
        Normal,
        Attack,
        Die
    }

    protected StateEnum state = StateEnum.Normal;

    // 動ける？
    public bool IsMovable
    {
        get
        {
            // 今がノーマルならば、true
            return state == StateEnum.Normal;
        }
    }

    // 攻撃できる？
    public bool IsAttackable
    {
        get
        {
            // 今がノーマルならば、true
            return state == StateEnum.Normal;
        }
    }

    // ライフの最大値
    [SerializeField]
    float lifeMax = 10;

    public float LifeMax
    {
        get
        {
            return lifeMax;
        }
    }
    public float Life       // 3/12/10追加情報

    {
        get
        {
            return life;
        }
    }
    // 現在のライフ
    float life;

    protected Animator animator;

    protected virtual void Start()
    {
        // 初期状態はライフ満タン
        life = LifeMax;
        animator = GetComponentInChildren<Animator>();

        // ライフゲージの表示開始
        LifeGaugeContainer.Instance.Add(this);
    }

    // 倒れた時の処理（派生クラスで実装します。派生クラスでは、override修飾子をつけます）
    protected virtual void OnDie()
    {
        // ライフゲージの表示終了
        LifeGaugeContainer.Instance.Remove(this);
    }

    // ダメージを受ける
    public void Damage(int damagePoint)
    {
        // 今、死んでる？
        if (state == StateEnum.Die)
        {
            // メソッドを抜ける
            return;
        }
        // ライフをダメージポイント分減らす
        life -= damagePoint;

        // 生きてる？
        if (life > 0)
        {
            return;
        }

        // 死んでる状態に移行
        state = StateEnum.Die;
        animator.SetTrigger("Die");

        OnDie();
    }

    // もし可能であれば、アタックステートに移行
    public void GoToAttackStateIfPossible()
    {
        // アタックできるのであれば？
        if (IsAttackable)
        {
            // アタック状態に移行
            state = StateEnum.Attack;
            animator.SetTrigger("Attack");
        }
    }

    // ノーマルに移れるのであれば？
    public void GoToNormalStateIfPossible()
    {
        // 今、死んでれば、抜ける
        if (state == StateEnum.Die)
        {
            return;
        }

        // ノーマル状態に移行
        state = StateEnum.Normal;
    }
}
