using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerTest : MonoBehaviour
{
    CharacterController characterController;

    // キャラクターの移動速度情報
    Vector3 moveVelocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Debug.Log(characterController.isGrounded);

        // ジョイスティックの上下・左右の入力を取得
        moveVelocity.x = Input.GetAxis("Horizontal");
        moveVelocity.z = Input.GetAxis("Vertical");

        // キャラクタの向きを移動方向に向ける
        transform.LookAt(transform.position + new Vector3(moveVelocity.x, 0, moveVelocity.z));

        // もし、ジョイスティックのJumpボタン（キーボードではスペース）を押されたら
        if (Input.GetButtonDown("Jump"))
        {
            // Update()ごとに３だけ上に移動する(この時点でisGraundedプロパティはfalseになる)
            moveVelocity.y = 3;
        }

        // もし、キャラクタが地上でなければ・・・
        if (!characterController.isGrounded)
        {
            // 重力によるY軸への移動を進める（マイナス方向）
            moveVelocity.y += Physics.gravity.y * 0.015f;
        }

        // キャラクタの移動
        characterController.Move(moveVelocity * 0.01f);
    }
}
