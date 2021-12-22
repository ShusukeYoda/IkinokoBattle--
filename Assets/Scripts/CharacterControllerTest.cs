using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerTest : MonoBehaviour
{
    //public struct Vector3 

    // Start is called before the first frame update
    void Start()
    {
        //characterController.Move(new Vector3(1, 2, 3));
    }

    // Update is called once per frame
    void Update()
    {
        CharacterController characterController = GetComponent<CharacterController>();
        characterController.SimpleMove(new Vector3(1, 2, 3));
    }
}
