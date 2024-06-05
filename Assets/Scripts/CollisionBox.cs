using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger enter");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("trigger exit");
    }

}
