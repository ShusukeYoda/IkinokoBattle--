using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]
//追記ｐ２００
[RequireComponent(typeof(PlayerStatus))]
[RequireComponent(typeof(MovingObjectAttack))]
//[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3;
    [SerializeField] float jumpPower = 3;

    CharacterController characterController;
    Transform transform1;
    Vector3 moveVelocity;

    //追記ｐ２００
    PlayerStatus status;
    MovingObjectAttack attack;

    bool isGrounded
    {
        get
        {
            //下向きのRayを作る
            var ray = new Ray(transform.position + new Vector3(0, 0.1f), Vector3.down);

            // Rayを可視化してデバッグに役立てる
            //Debug.DrawLine(ray.origin, ray.origin - new Vector3(0, 0.2f), Color.red);

            RaycastHit[] raycasthits = new RaycastHit[1];

            //hitCountが１以上ならば地面がある
            int hitcount = Physics.RaycastNonAlloc(ray, raycasthits, 0.2f);
            bool result = hitcount >= 1;
            return result;
        }
    }

    [SerializeField] Animator animator;  //追加

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        //追記ｐ２００
        status = GetComponent<PlayerStatus>();
        attack = GetComponent<MovingObjectAttack>();

        transform1 = transform;
    }

    // Update is called once per frame
    void Update()
    {
        //もし動けるならば（状態ノーマルならば）
        if (status.IsMovable)
        {
            moveVelocity.x = Input.GetAxis("Horizontal") * moveSpeed;
            moveVelocity.z = Input.GetAxis("Vertical") * moveSpeed;
            //移動方向を向く
            transform1.LookAt(transform1.position + new Vector3(
                moveVelocity.x, 0, moveVelocity.z));
        }
        else
        {
            moveVelocity.x = 0;
            moveVelocity.z = 0;
        }

        if (characterController.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log("ジャンプ");
                moveVelocity.y = jumpPower;
            }
        }
        else
        {
            moveVelocity.y += Physics.gravity.y * Time.deltaTime;
        }

        // キャラクタの移動
        characterController.Move(moveVelocity * Time.deltaTime);

        //移動スピードをanimatorに反映
        animator.SetFloat("MoveSpeed", new Vector3       //追加
            (moveVelocity.x, 0, moveVelocity.z).magnitude);


        // UIをクリックした時は、戻る（次の剣をふる処理はしない）
        // https://nn-hokuson.hatenablog.com/entry/2017/07/12/220302
        // スマートホンでも対応
#if UNITY_EDITOR　|| UNITY_STANDALONE || UNITY_STANDALONE_OSX
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
#else 
    if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) {
        return;
    }
#endif

        //Debug.Log(characterController.isGrounded ? "地上" : "空中");
        //Debug.Log(isGrounded);

        //追記ｐ２００
        if (Input.GetButtonDown("Fire1"))
        {
            attack.AttackIfPossible();
        }

        //アプリケーションの終了
        if (Input.GetButtonDown("Cancel"))
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
          Application.Quit();
#endif
        }
    }
}
//        if (!characterController.isGrounded)
// ジョイスティックの上下・左右の入力を取得
//moveVelocity.x = Input.GetAxis("Horizontal");
//moveVelocity.z = Input.GetAxis("Vertical");
// キャラクタの向きを移動方向に向ける
//transform.LookAt(transform.position + new Vector3(moveVelocity.x, 0, moveVelocity.z));
// Update()ごとに３だけ上に移動する(この時点でisGraundedプロパティはfalseになる)
//moveVelocity.y = 2;
/*
// もし、キャラクタが地上でなければ・・・
if (!isGrounded)
{
    // 重力によるY軸への移動を進める（マイナス方向）
    moveVelocity.y += Physics.gravity.y * 0.015f;
}
*/