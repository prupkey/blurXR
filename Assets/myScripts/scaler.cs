//used for scaling of walls to match real world walls.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scaler : MonoBehaviour
{
    public GameObject manager;
    public SaveAndLoader sal;

    
    public GameObject rightNode;
    public GameObject floor;
    public GameObject parent;
    public bool building = true;
    private Vector3 initialScale;
    private Vector3 conFloor;
    private Vector3 conHeight;
    private int stage = 0;

    //called on first frame once spawned.
    private void Start()
    {
        initialScale = transform.localScale;
        rightNode = GameObject.Find("RightNode");
        floor = GameObject.Find("GroundPlane");
        parent = GameObject.Find("WorldPos");

        manager = GameObject.Find("Manager");
        sal = manager.GetComponent<SaveAndLoader>();

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
    void Update()
    {
        stager();
        if (OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick)) // if tool changes then script is disabled.
        {
            enabled = false;
        }            
        if (stage == 0) // after first point is marked it looks for second one and scales based on where your controler is.
        {
            conFloor = new Vector3(rightNode.transform.position.x, floor.transform.position.y, rightNode.transform.position.z);
            float distance = Vector3.Distance(transform.position, conFloor);
            transform.localScale = new Vector3(initialScale.x, initialScale.y, distance);
            transform.forward = transform.position - conFloor;
            transform.LookAt(conFloor);           
        }
        else if (stage == 1) // simply scales the height of the wall to the height between floor and controller.
        {
            conHeight = new Vector3(transform.position.x, rightNode.transform.position.y, transform.position.z);
            float distance = Vector3.Distance(transform.position, conHeight);
            transform.localScale = new Vector3(initialScale.x, distance, initialScale.z);
        }
        else if (stage == 2) // wall created, kill script.
        {

            transform.SetParent(parent.transform);

            ObjectSaveData obSD = new ObjectSaveData();

            obSD.position = gameObject.transform.position;
            obSD.rotation = gameObject.transform.rotation;
            obSD.localscale = gameObject.transform.localScale;


            sal.saveListData.Add(obSD);
            
            //add to save list

            enabled = false;
        }
    }
}
