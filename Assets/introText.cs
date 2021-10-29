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
    public GameObject conRight;
    public GameObject Target;
    public GameObject floor;
    public Material mat;
    public GameObject anchor;


    private LineRenderer lineRenderer;

    void Start()
    {
        inIntro = true;
    }
    void stageCount()
    {
        if (OVRInput.GetUp(OVRInput.Button.One) && inIntro == true)
        {
            stage = stage + 1;
        }
    }
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

    void floorTrack()
    {
        floor.transform.position = new Vector3(player.transform.position.x, floor.transform.position.y, player.transform.position.z);
    }

    void stageCheck()
    {
        if (stage == 1)
        {
            text3d = GetComponent<TextMesh>();
            GetComponent<TextMesh>().fontSize = 55;

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

            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                reAlign();
            }
        }        
        else if (stage == 3)
        {
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

    void leaveMenu()
    {
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {           
            inIntro = false;
        }
    }

    void openControlMenu()
    {
        if (OVRInput.GetDown(OVRInput.Button.Start))
        {
            inIntro = true;
            stage = 4;
        }
    }

    void menuCleaner()
    {
        if (inIntro == false)
        {
            text3d = GetComponent<TextMesh>();
            text3d.text = "";
        }
    }

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




