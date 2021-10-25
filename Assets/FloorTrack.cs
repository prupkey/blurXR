using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTrack : MonoBehaviour
{
    public GameObject player;
    public GameObject floor;
    public int stage;
    // Update is called once per frame
    void Update()
    {
        if (stage == 1)
        {
            floor.transform.position = new Vector3(player.transform.position.x, floor.transform.position.y, player.transform.position.z);
        }

    }
}
