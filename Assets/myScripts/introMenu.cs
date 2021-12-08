using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introMenu : MonoBehaviour
{
    public GameObject playerHead;
    public GameObject quadSpawner;
    public GameObject controlTag;
    public int aPresses;
    private trackingSetup beginTracking;

    private QuadSpawner quadComp;


    // Start is called before the first frame update
    void Start()
    {
        quadComp = quadSpawner.GetComponent<QuadSpawner>();
        quadComp.enabled = false;
        aPresses = 0;
        beginTracking = GetComponent<trackingSetup>();
    }
    void markStart()
    {
        if (OVRInput.GetDown(OVRInput.Button.Start))
        {
            controlTag.SetActive(false);
        }
    }

    void aPressCounter()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            aPresses = aPresses + 1;
        }
    }

    void aPressStages()
    {
        if (aPresses == 1)
        {
            beginTracking.enabled = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        markStart();
        aPressCounter();
        aPressStages();
    }
}
