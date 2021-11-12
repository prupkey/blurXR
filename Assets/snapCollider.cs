using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snapCollider : MonoBehaviour
{

    private void Start()
    {
        gameObject.name = "SnapOFF";
    }
    void OnCollisionEnter(Collision other)
    {
        gameObject.name = "SnapON";
    }
    private void OnCollisionExit(Collision collision)
    {
        gameObject.name = "SnapOFF";
    }
}
