using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadSpawner : MonoBehaviour
{
    private int wallStage = 0;
    private int boxStage = 0;
    public int stage = 0;
    public int tool = 0;

    Vector3 pointA = new Vector3(0, 0);
    Vector3 pointB = new Vector3(0, 0);
    Vector3 pointC = new Vector3(0, 0);    

    public GameObject spawnTest;
    public GameObject physicsObj;
    public GameObject cubeOutline;

    public GameObject physicsObj2;
    public GameObject sphereOutline;

    public GameObject wall;
    public GameObject box;
    public GameObject floor;
    public GameObject conRight;
    public GameObject conLeft;
    public GameObject leftNode;
    public GameObject toolName;

    TextMesh toolText;

    private Vector3 InitialScale;

    public bool buildingWall = false;

    Vector3[] verticies = new Vector3[3];
    Vector2[] uv = new Vector2[3];
    int[] triangles = new int[3];

    private void Start()
    {
        toolText = toolName.GetComponent<TextMesh>();
    }

    void createQuad()
    {
        if(OVRInput.GetUp(OVRInput.Button.Two))
        {
            if (stage == 0)
            {
                pointA = conRight.transform.position;
                stage = stage + 1;
                Debug.LogError("stage 1 - point stored");
            }
            else if (stage == 1)
            {
                pointB = conRight.transform.position;
                stage = stage + 1;
                Debug.LogError("stage 2 - point stored");
            }
            else if (stage == 2)
            {
                pointC = conRight.transform.position;
                
                Debug.LogError("stage 3 - point stored");

                Mesh mesh = new Mesh();

                verticies[0] = pointA;
                verticies[1] = pointB;
                verticies[2] = pointC;

                uv[0] = new Vector2(0, 0);
                uv[1] = new Vector2(0, 1);
                uv[2] = new Vector2(1, 1);

                triangles[0] = 0;
                triangles[1] = 1;
                triangles[2] = 2;

                mesh.vertices = verticies;
                mesh.uv = uv;
                mesh.triangles = triangles;

                GetComponent<MeshFilter>().mesh = mesh;

                Debug.LogError("SPAWNED");
                stage = 0;

                spawnObjectTest(pointA);
                spawnObjectTest(pointB);
                spawnObjectTest(pointC);

            }
            else
            {
                stage = 0;
                Debug.LogError("fixed stage -> now 0");
            }
        }
        else
        {
            Debug.LogError("awaiting input");
        }
    }

    void createWall()
    {
        if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger) && wallStage == 0) 
        {
            Vector3 wallPointA = conRight.transform.position;
            GameObject wallNew = Instantiate(wall) as GameObject;
            wallNew.transform.position = wallPointA;
            wallNew.transform.position = new Vector3(wallNew.transform.position.x, floor.transform.position.y, wallNew.transform.position.z);

            wallStage = wallStage + 1;

        }
        else if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger) && wallStage == 1)
        {
            wallStage = wallStage + 1;
        }
        else if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger) && wallStage == 2)
        {
            wallStage = 0;
        }

    }
    void createBox()
    {
        if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger) && boxStage == 0)
        {
            Vector3 boxPoint = conRight.transform.position;
            GameObject boxNew = Instantiate(box) as GameObject;
            boxNew.transform.position = boxPoint;
            boxNew.transform.position = new Vector3(boxNew.transform.position.x, floor.transform.position.y, boxNew.transform.position.z);

            boxStage = boxStage + 1;

        }
        else if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger) && boxStage == 1)
        {
            boxStage = boxStage + 1;
        }
        else if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger) && boxStage == 2)
        {
            boxStage = boxStage + 1;
        }
        else if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger) && boxStage == 3)
        {
            boxStage = 0;
        }
    }

    private void spawnObjectTest(Vector3 point)
    {
        GameObject a = Instantiate(spawnTest) as GameObject;
        a.transform.position = point;
    }

    private void spawnPhysicsObject(Vector3 point)
    {
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            Renderer rend;
            rend = cubeOutline.GetComponent<Renderer>();
            rend.enabled = true;
        }
        if (OVRInput.GetUp(OVRInput.Button.Three))
        {
            Renderer rend;
            rend = cubeOutline.GetComponent<Renderer>();
            rend.enabled = false;

            GameObject b = Instantiate(physicsObj) as GameObject;
            b.transform.position = point;           

        }
    }
    
    private void spawnPhysicsSphere(Vector3 point)
    {
        if (OVRInput.GetDown(OVRInput.Button.Four))
        {
            Renderer rend;
            rend = sphereOutline.GetComponent<Renderer>();
            rend.enabled = true;
        }
        if (OVRInput.GetUp(OVRInput.Button.Four))
        {
            Renderer rend;
            rend = sphereOutline.GetComponent<Renderer>();
            rend.enabled = false;

            GameObject c = Instantiate(physicsObj2) as GameObject;
            c.transform.position = point;
            
        }
    }

    private void toolSelection()
    {
        if (OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick))
        {
            tool = tool + 1;            
        }

        if (tool == 0) //wall tool
        {
            toolText.text = "WALL";
            createWall();
            boxStage = 0;
        }
        else if (tool == 1) //box tool selected
        {
            toolText.text = "BOX";
            wallStage = 0;
            createBox();
        }
        else if (tool == 2)
        {
            tool = 0;
        }
    }

    
    void Update()
    {

        toolSelection();
        createQuad();
        spawnPhysicsObject(leftNode.transform.position);
        spawnPhysicsSphere(leftNode.transform.position);   
    }
}
