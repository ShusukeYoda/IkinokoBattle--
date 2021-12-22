using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTest : MonoBehaviour
{
    [SerializeField]
    UnityEvent<Collider> unityEvent;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            unityEvent.Invoke(GetComponent<Collider>());
        }
    }
}


/*
{
    [SerializeField]
    TriggerEvent onTriggerStay = new TriggerEvent();

    void OnTriggerStay(Collider other)
    {
        onTriggerStay.Invoke(other);
    }
}
*/
/*
 　[SerializeField]
    UnityEvent<Collider> unityEvent;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            unityEvent.Invoke(GetComponent<Collider>());
        }
    }
*/
/*
[Serializable]
public class TriggerEvent : UnityEvent<Collider>
{
}
    [SerializeField]
    UnityEvent<Collider> unityEvent;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            unityEvent.Invoke(GetComponent<Collider>());
        }
    }
 */
/*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            unityEvent.Invoke(gameObject);
        }
    }
 */
/*
    [SerializeField]
    UnityEvent<string> unityEvent;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            unityEvent.Invoke("こんにちは");    //()内・引数
        }
    }
 */
/*
    [SerializeField]
    UnityEvent unityEvent;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            unityEvent.Invoke();    //Invoke は呼び出すの意
        }
    }
*/