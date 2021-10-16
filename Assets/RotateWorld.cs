using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWorld : MonoBehaviour
{
    public GameObject conLeft;
    public GameObject conRight;
    public GameObject Target;
    bool align = true;
    public int frameCount = 0;

    // Start is called before the first frame update
    void reAlign()
    {
        Vector3 positionA = conLeft.transform.position;
        Vector3 positionB = conRight.transform.position;

        Target.transform.rotation = Quaternion.LookRotation(positionB - positionA, Vector3.up);
        Target.transform.position = positionA;

    }


    // Update is called once per frame

    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.One))
        {
            reAlign();
            Debug.LogError("realigning");
        }
    }
}
