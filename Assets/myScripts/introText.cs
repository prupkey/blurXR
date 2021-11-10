// script is used to go through the intro text for setting up a space. (also tracks the floor once marked by player. likely to move to its own script in the future)
// one known bug: when pressing button ovr input button 4, stage gets set to 4 for some reason. 
// known issue within every script: OVRInput is redlined but still works. After some work on the code one day, and not being able to trace what I did, all scipts I wrote had this issue show up. (maybe fixed?)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introText : MonoBehaviour
{
    public GameObject player;
    TextMesh text3d;
    public int stage = 0;
    public bool inIntro;
    public GameObject conLeft;
    public GameObject leftMesh;
    public GameObject rightMesh;
    public GameObject conRight;
    public GameObject Target;
    public GameObject floor;
    public Material mat;
    public Material enabledMat;
    public Material nullMat;
    public GameObject anchor;

    private LineRenderer lineRenderer;

    // Start runs on first frame.
    void Start()
    {
        inIntro = true;
    }
    //changes the stage of the intro
    void stageCount()
    {
        if (OVRInput.GetUp(OVRInput.Button.One) && inIntro == true)
        {
            stage = stage + 1;
        }
    }
    // spawms 2 visual anchor points over existing anchor points in the real world. parented to the rotation of the world.
    void reAlign()
    {
        Vector3 positionA = conLeft.transform.position;
        Vector3 positionB = conRight.transform.position;

        Target.transform.rotation = Quaternion.LookRotation(positionB - positionA, Vector3.up);
        Target.transform.position = positionA;

        lineRenderer = new GameObject("Line").AddComponent<LineRenderer>();
        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.black;
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;
        lineRenderer.transform.SetParent(Target.transform);

        //For drawing line in the world space, provide the x,y,z values
        lineRenderer.SetPosition(0, positionA); 
        lineRenderer.SetPosition(1, positionB);
        lineRenderer.material = mat;

        GameObject a = Instantiate(anchor) as GameObject;
        GameObject b = Instantiate(anchor) as GameObject;
        a.transform.SetParent(Target.transform);
        b.transform.SetParent(Target.transform);
        a.transform.position = positionA;
        b.transform.position = positionB;
    }
    // tracks the floor to the player
    void floorTrack()
    {
        floor.transform.position = new Vector3(player.transform.position.x, floor.transform.position.y, player.transform.position.z);
    }
    // checks the intro stage and shows text accoringly
    // also goes through the motions of setting up the world by setting floor height and setting up the anchor points.
    void stageCheck()
    {
        if (stage == 1)
        {
            text3d = GetComponent<TextMesh>();
            GetComponent<TextMesh>().fontSize = 55;
            rightMesh.GetComponent<SkinnedMeshRenderer>().material = enabledMat;

            text3d.text = "PLACE RIGHT HAND ON GROUND\n PRESS A";
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                Vector3 cord = conRight.transform.position;
                floor.transform.position = cord;
            }
        }        
        else if (stage == 2)
        {
            text3d = GetComponent<TextMesh>();
            text3d.text = "PLACE CONTROLLERS\nON ANCHOR STATIONS\nPRESS A";
            leftMesh.GetComponent<SkinnedMeshRenderer>().material = enabledMat; //   THESE BREAK MY CODE FOR SOME REASON
            //would like to use these lines to change the material of the controllers to 

            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                reAlign();
            }
        }        
        else if (stage == 3)
        {
            rightMesh.GetComponent<SkinnedMeshRenderer>().material = nullMat;
            leftMesh.GetComponent<SkinnedMeshRenderer>().material = nullMat;
            text3d = GetComponent<TextMesh>();
            text3d.text = "CONTROLS - PRESS A\nCLOSE - PRESS B";
            
        }
        else if(stage == 4)
        {
            text3d = GetComponent<TextMesh>();
            text3d.text = "SPAWN CUBE - X\nSPAWN BALL - Y\nPRESS  A";
        }
        else if (stage == 5)
        {
            text3d = GetComponent<TextMesh>();
            text3d.text = "CLICK RIGHT THUMB\nTO CHANGE TOOLS\nPRESS TRIGGER TO BUILD\nCLOSE - PRESS B";
        }

        else if (stage == 6)
        {
            text3d.text = "";
            inIntro = false;        
        }
    }
    // closes menu when pressing b
    void leaveMenu()
    {
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {           
            inIntro = false;
        }
    }
    // opens menu when pressing start
    void openControlMenu()
    {
        if (OVRInput.GetDown(OVRInput.Button.Start))
        {
            inIntro = true;
            stage = 4;
        }
    }
    //if the intro is false then should be cleaned
    void menuCleaner()
    {
        if (inIntro == false)
        {
            text3d = GetComponent<TextMesh>();
            text3d.text = "";
        }
    }
   //called every frame.
    void Update()
    {
        floorTrack();        
        stageCount();
        openControlMenu();
        stageCheck();
        leaveMenu();
        menuCleaner();    
    }
}




