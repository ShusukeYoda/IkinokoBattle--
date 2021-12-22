using System;
using UnityEngine;
using UnityEngine.Events;

public class CllisionDetector : MonoBehaviour
{
    //リスト7.9書き換え
    [SerializeField]
    TriggerEvent onTriggerEnter = new TriggerEvent();
    [SerializeField]
    TriggerEvent onTriggerStay = new TriggerEvent();

    private void OnTriggerEnter(Collider other)
    {
        onTriggerEnter.Invoke(other);
    }

    private void OnTriggerStay(Collider other)
    {
        onTriggerStay.Invoke(other);
    }

    [Serializable]
    public class TriggerEvent : UnityEvent<Collider>
    {
    }
}
