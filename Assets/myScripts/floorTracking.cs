using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floorTracking : MonoBehaviour
{

    public GameObject floor;
    public GameObject player;

    void Update()
    {
            floor.transform.position = new Vector3(player.transform.position.x, floor.transform.position.y, player.transform.position.z);        
    }
}
