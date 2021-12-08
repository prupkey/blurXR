using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class trackingSetup : MonoBehaviour
{
    public GameObject player;
    public GameObject tmpGameObject;
    public int stage = 0;
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
    public GameObject menu;
    public GameObject quadSpawner;
 
    private LineRenderer lineRenderer;
    private QuadSpawner quadComp;
    private TextMeshPro tmpComp;

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

    void stageCount()
    {
        if (OVRInput.GetUp(OVRInput.Button.One))
        {
            stage = stage + 1;
        }
    }

    void introStages()
    {
        if (stage == 1)   // changes text // moves floor position // flashes right hand
        {           
            rightMesh.GetComponent<SkinnedMeshRenderer>().material = enabledMat;
            tmpComp.text = "place your right hand\non the ground\n\nthen press A";
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                Vector3 cord = conRight.transform.position;
                floor.transform.position = cord;
            }
        }

        else if (stage == 2) // change text // realigns world // flashes left hand
        {
            tmpComp.text = "place both hands on anchor stations\n\nthen press A";
            leftMesh.GetComponent<SkinnedMeshRenderer>().material = enabledMat;
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                reAlign();
            }
        }
        else if (stage == 3) // change text // 
        {
            tmpComp.text = "world aligned. begin testing\nwhen finished please restart app\n\npress A to close";
            leftMesh.GetComponent<SkinnedMeshRenderer>().material = nullMat;
            rightMesh.GetComponent<SkinnedMeshRenderer>().material = nullMat;
            
        }
        else if (stage >= 4) // close menu
        {
            menu.SetActive(false);            
            quadComp.enabled = true;

            enabled = false;            

        }
    }

    private void Start()
    {
        
        tmpComp = tmpGameObject.GetComponent<TextMeshPro>();
        quadComp = quadSpawner.GetComponent<QuadSpawner>();        
        quadComp.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        stageCount();
        introStages();

    }
}
