using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    LineRenderer lineRenderer;
    public GameObject button;

    void Start()
    {
       
        lineRenderer = new GameObject("Line") .AddComponent<LineRenderer>();
        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.black;
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = false;



        lineRenderer.SetPosition(0, transform.position); //x,y and z position of the starting point of the line
        lineRenderer.SetPosition(1, button.transform.position); //x,y and z position of the end point of the line
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
