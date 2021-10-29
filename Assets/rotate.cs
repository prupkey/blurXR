using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    public GameObject head;

    // Update is called once per frame
    void Update()
    {
        transform.position = head.transform.position;    }
}
