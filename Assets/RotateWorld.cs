using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWorld : MonoBehaviour
{
    public GameObject conLeft;
    public GameObject conRight;
    public GameObject Target;
    public int stage = 0;
    public GameObject floor;
    public GameObject player;
    public bool inIntro;

    // Start is called before the first frame update
    void reAlign()
    {
        Vector3 positionA = conLeft.transform.position;
        Vector3 positionB = conRight.transform.position;

        Target.transform.rotation = Quaternion.LookRotation(positionB - positionA, Vector3.up);
        Target.transform.position = positionA;

    }
    void floorTrack()
    {
        floor.transform.position = new Vector3(player.transform.position.x, floor.transform.position.y, player.transform.position.z);
    }
    void stageCount()
    {
        if (OVRInput.GetUp(OVRInput.Button.One) && inIntro == true)
        {
            stage = stage + 1;
        }
    }
    void Start()
    {
        inIntro = true;
    }

    // Update is called once per frame

    void Update()
    {
       
        if (stage == 2)
        {
            Vector3 cord = conLeft.transform.position;
            floor.transform.position = cord;
            stage = stage + 1;

        }
        if (stage == 4)
        {
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                reAlign();
                inIntro = false;
                stage = stage + 1;
            }
            floorTrack();
        }
    }


}
