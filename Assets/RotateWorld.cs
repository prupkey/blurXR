using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWorld : MonoBehaviour
{
    public GameObject conLeft;
    public GameObject conRight;
    public GameObject Target;
    public int frameCount = 0;
    public int stage = 0;
    public GameObject floor;
    public GameObject player;

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

    // Update is called once per frame

    void Update()
    {
       
        if (stage == 0)
        {

            if (OVRInput.GetDown(OVRInput.Button.Start))
            {

                Vector3 cord = conLeft.transform.position;
                floor.transform.position = cord;

                Debug.LogError("FLOOR SET");
                stage = stage + 1;




            }
        }
        if (stage == 1)
        {
            if (OVRInput.GetDown(OVRInput.Button.Start))
            {
                reAlign();
            }
            floorTrack();
        }
    }
}
