using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// QuadSpawner is used to spawn all objects. This includes physics objects and mapping objects. 
// known issue within every script: OVRInput is redlined but still works. After some work on the code one day, and not being able to trace what I did, all scipts I wrote had this issue show up.
// this is not a breaking issue (at least not known) but it makes degugging those calls harder.


public class QuadSpawner : MonoBehaviour
{
    //all vars used. If I understand correctly having this many global vars can slow down the script, have not run into any issues with speed yet though.
    private int wallStage = 0;
    private int boxStage = 0;
    public int stage = 0;
    public int tool = 0;

    Vector3 pointA = new Vector3(0, 0);
    Vector3 pointB = new Vector3(0, 0);
    Vector3 pointC = new Vector3(0, 0);
    Vector3 rotation;

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
    public GameObject rightNode;
    public GameObject toolName;

    public Material quadMat;

    public bool snapState = true;

    TextMesh toolText;

    private Vector3 InitialScale;

    public bool buildingWall = false;

    Vector3[] verticies = new Vector3[3];
    Vector2[] uv = new Vector2[3];
    int[] triangles = new int[3];

    // Start runs on first frame.
    private void Start()
    {
        toolText = toolName.GetComponent<TextMesh>();
    }
    // Used to create Quads (a unity mesh object) 
    //Known issues: upon creating a quad it replaces the old one. It seems to be overwriting the older data and reusing the same quad. Maybe I need to create a List or Array of quads and add 
    // to it. I did some research on this but I am not too sure on how to do this.

    public Vector3 Snap(Vector3 cords)
    {
        if (snapState == true)
        {
            GameObject snapPoint = GameObject.Find("SnapON");
            return cords;
        }
        else
            return cords;            
    }

    void createQuad()
    {
        if(OVRInput.GetUp(OVRInput.Button.Two))
        {
            if (stage == 0)
            {
                pointA = rightNode.transform.position;
                Snap(pointA);
                stage = stage + 1;
                Debug.LogError("stage 1 - point stored");
                spawnObjectTest(pointA);
            }
            else if (stage == 1)
            {
                pointB = rightNode.transform.position;
                Snap(pointB);
                stage = stage + 1;
                Debug.LogError("stage 2 - point stored");
                spawnObjectTest(pointB);
            }
            else if (stage == 2)
            {
                pointC = rightNode.transform.position;
                Snap(pointC);

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

                var newQuad = new GameObject("Quad");
                MeshFilter mf = newQuad.gameObject.AddComponent<MeshFilter>() as MeshFilter;
                MeshRenderer mr = newQuad.gameObject.AddComponent<MeshRenderer>() as MeshRenderer;
                newQuad.GetComponent<MeshRenderer>().material = quadMat;
                newQuad.GetComponent<MeshFilter>().mesh = mesh;

                Debug.LogError("SPAWNED");
                stage = 0;

                // a visual marker in space to show the points used. mostly for debugging.               
                
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
    

    // createWall is used to create a virtual wall matches a physical wall. It spawns a premade wall object at floor level and then the script for it begins on Start within the wallObject. 
    // stages match between the two scripts to make sure they are in sync. 
    // stages include: marking a point, marking a second point and scaling the wall accoringly, fixing the distance and oriantation between the two, and finally scaling the height.
    void createWall()
    {
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) && wallStage == 0) 
        {
            Vector3 wallPointA = rightNode.transform.position;
            GameObject wallNew = Instantiate(wall) as GameObject;
            wallNew.transform.position = wallPointA;
            wallNew.transform.position = new Vector3(wallNew.transform.position.x, floor.transform.position.y, wallNew.transform.position.z);

            wallStage = wallStage + 1;

        }
        else if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) && wallStage == 1)
        {
            wallStage = wallStage + 1;
        }
        else if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) && wallStage == 2)
        {
            wallStage = 0;
        }

    }
    // similar to createWall but is used to mark out obstacles such as tables and desk. Goes through the same stages as create wall, but now scales along the width before scaling the height. 
    // known issues within the boxscaler script: scaling direction based on the players controller is wrong when scaling the width.
    void createBox()
    {
        if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger) && boxStage == 0)
        {
            Vector3 boxPoint = rightNode.transform.position;
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
    // used to mark quad points for debugigng.
    private void spawnObjectTest(Vector3 point)
    {
        GameObject a = Instantiate(spawnTest) as GameObject;
        a.transform.position = point;
    }
    // spawns a cube in front of left controller. while holding down the button you can see where it is going to be placed before lifting your finger. Cubes have physics that interact with 
    // floor, walls, and boxs
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
            rotation = new Vector3(conLeft.transform.position.x, leftNode.transform.position.y, conLeft.transform.position.z);
            b.transform.LookAt(rotation, transform.forward);

        }
    }

    // same as spawnPhysicsObject but spawns a sphere that can roll.

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

    //using right thumbstick you can cycle between the tools for mapping. Right now it contains wall and box tool. It also shows the name of the tool next to the right hand contoller.

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
    //called every frame.
    void Update()
    {
        toolSelection();
        createQuad();
        spawnPhysicsObject(leftNode.transform.position);
        spawnPhysicsSphere(leftNode.transform.position);   
    }
}
