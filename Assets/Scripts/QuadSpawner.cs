using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadSpawner : MonoBehaviour
{
    public GameObject conRight;
    public int stage = 0;
    OVRInput.Controller controller = OVRInput.Controller.RTouch;
    Vector3 pointA = new Vector3(0, 0);
    Vector3 pointB = new Vector3(0, 0);
    Vector3 pointC = new Vector3(0, 0);    // set 0s
    bool solidMesh = false; // set false

    // OVRInput.Controller controller = OVRInput.Controller.RTouch;

    void getpostitions()
    {
        if(OVRInput.GetUp(OVRInput.Button.Two))
        {
            if (stage == 0)
            {
                Vector3 pointA = conRight.transform.position;
                stage = stage + 1;
                Debug.LogError("stage 1 - point stored");
            }
            else if (stage == 1)
            {
                Vector3 pointB = conRight.transform.position;
                stage = stage + 1;
                Debug.LogError("stage 2 - point stored");
            }
            else if (stage == 2)
            {
                Vector3 pointC = conRight.transform.position;
                stage = 0;
                solidMesh = true;
                Debug.LogError("stage 3 - point stored");

            }
            else
            {
                stage = 0;
                solidMesh = false;
                Debug.LogError("fixed stage -> now 0");
            }
        }
        else
        {
            Debug.LogError("awaiting input");
        }
    }




    void Update()
    {
        Debug.LogError("idle");
        getpostitions();

        if (solidMesh == true)
        {
            Mesh mesh = new Mesh();

            Vector3[] verticies = new Vector3[3];
            Vector2[] uv = new Vector2[3];
            int[] triangles = new int[3];

            verticies[0] = pointA;
            verticies[1] = pointB;
            verticies[2] = pointC;

            uv[0] = new Vector2(0, 0);
            uv[0] = new Vector2(0, 1);
            uv[0] = new Vector2(1, 1);

            triangles[0] = 0;
            triangles[1] = 1;
            triangles[2] = 2;

            mesh.vertices = verticies;
            mesh.uv = uv;
            mesh.triangles = triangles;

            GetComponent<MeshFilter>().mesh = mesh;
            solidMesh = false;
            Debug.LogError("SPAWNED");
        }
    }
}
