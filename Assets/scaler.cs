using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scaler : MonoBehaviour
{
    public GameObject conRight;
    public GameObject floor;
    public bool building = true;
    private Vector3 initialScale;
    private Vector3 conFloor;
    private Vector3 conHeight;
    private int stage = 0;

    private void Start()
    {
        initialScale = transform.localScale;
        conRight = GameObject.Find("RightHandAnchor");
        floor = GameObject.Find("GroundPlane");

    }
    void stager()
    {
        if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger))
        {
            stage = stage + 1;
            initialScale = transform.localScale;
        }
    }
    void Update()
    {
        stager();
        if (OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick))
        {
            enabled = false;
        }            
        if (stage == 0)
        {
            conFloor = new Vector3(conRight.transform.position.x, floor.transform.position.y, conRight.transform.position.z);
            float distance = Vector3.Distance(transform.position, conFloor);
            transform.localScale = new Vector3(initialScale.x, initialScale.y, distance);
            transform.forward = transform.position - conFloor;
            transform.LookAt(conFloor);           
        }
        else if (stage == 1) 
        {
            conHeight = new Vector3(transform.position.x, conRight.transform.position.y, transform.position.z);
            float distance = Vector3.Distance(transform.position, conHeight);
            transform.localScale = new Vector3(initialScale.x, distance, initialScale.z);
        }
        else if (stage == 2)
        {
            enabled = false;
        }
    }
}
