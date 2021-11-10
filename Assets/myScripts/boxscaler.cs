//used for scaling of boxes to match real world obstables.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxscaler : MonoBehaviour
{
    public GameObject conRight;
    public GameObject floor;
    public GameObject parent;
    public bool building = true;
    private Vector3 initialScale;
    private Vector3 conFloor;
    private Vector3 conHeight;
    private Vector3 conWidth;
    private int stage = 0;
    private float angle;

    //called on first frame once spawned.
    private void Start()
    {
        initialScale = transform.localScale;
        conRight = GameObject.Find("RightHandAnchor");
        floor = GameObject.Find("GroundPlane");
        parent = GameObject.Find("WorldPos");

    }
    //changes the stage of the scaling action, every time a stage is changed the scale is saved.
    void stager()
    {
        if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger))
        {
            stage = stage + 1;
            initialScale = transform.localScale;
        }
    }
    // called every frame.
    // scales along one direction on first stage, then scales along the width in next stage. Width scaling is a known issue (shown below) 
    void Update()
    {
        stager();

        if (OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick)) // if tool changes then script is disabled.
        {
            enabled = false;
        }
        if (stage == 0) // after first point is marked it looks for second one and scales based on where your controler is.
        {
            conFloor = new Vector3(conRight.transform.position.x, floor.transform.position.y, conRight.transform.position.z);
            float distance = Vector3.Distance(transform.position, conFloor);
            transform.localScale = new Vector3(initialScale.x, initialScale.y, distance);
            transform.forward = transform.position - conFloor;
            transform.LookAt(conFloor);
        }
        else if (stage == 1) // KNOWN ISSUE after 2 points are marked it begins to scale along the width based on location of right hand contoller. I want to scale along the axis created by the two points spawned above but scales along the wrong axis. Works but you need to know how to move your controller. maybe use absolute val?            
        {
            conWidth = new Vector3(conRight.transform.position.x, floor.transform.position.y, conRight.transform.position.z);
            float distance = Vector3.Distance(transform.position, conWidth);

            angle = Vector3.Angle(conWidth, transform.position);

            if (angle > 0)
            {
                transform.localScale = new Vector3(distance, initialScale.y, initialScale.z);
            }
            else if (angle < 0)
            {
                transform.localScale = new Vector3(-distance, initialScale.y, initialScale.z);
            }
           
        }
        else if (stage == 2) // simply scales the height of the box to the height between floor and controller.
        {
            conHeight = new Vector3(transform.position.x, conRight.transform.position.y, transform.position.z);
            float distance = Vector3.Distance(transform.position, conHeight);
            transform.localScale = new Vector3(initialScale.x, distance, initialScale.z);
        }
        else if (stage == 3) // box created, kill script.
        {
            transform.SetParent(parent.transform);
            enabled = false;
        }

    }


}
