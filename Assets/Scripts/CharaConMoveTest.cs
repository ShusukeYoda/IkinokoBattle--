using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaConMoveTest : MonoBehaviour
{
    CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        characterController.SimpleMove(new Vector3(h * 0.5f, 0, v * 0.5f));
    }
}
