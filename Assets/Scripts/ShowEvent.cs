using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowEvent : MonoBehaviour
{    public void Show(Collider obj)
    {
        Debug.Log(obj.name +"をもらった");
    }
}

/*
{    public void Show(Collider obj)
    {
        Debug.Log(obj.name +"をもらった");
    }
}
 */
/*
{    public void Show(GameObject obj)
    {
        Debug.Log(obj.name +"をもらった");
    }
}

 */