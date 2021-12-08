using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class inputController : MonoBehaviour
{
    public GameObject hudR;
    public GameObject hudL;
    private bool hudState;


    private void Start()
    {
        hudState = false;
        hudR.SetActive(false);
        hudL.SetActive(false);
    }
    void Update()
    {
        if (OVRInput.GetUp(OVRInput.Button.Start) && hudState == true)
        {
            hudState = false;
            hudL.SetActive(false);
            hudR.SetActive(false);
        }
        else if (OVRInput.GetUp(OVRInput.Button.Start) && hudState == false)
        {
            hudState = true;
            hudL.SetActive(true);
            hudR.SetActive(true);
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryThumbstick))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
