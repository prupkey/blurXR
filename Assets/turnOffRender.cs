using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnOffRender : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Renderer rend;
        rend = GetComponent<Renderer>();
        rend.enabled = false;
    }
}
