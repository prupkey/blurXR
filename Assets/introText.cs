using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introText : MonoBehaviour
{
    public GameObject player;
    TextMesh text3d;


    void Start()
    {
        StartCoroutine(IntroText());
    }

   public IEnumerator IntroText()
    {
        yield return new WaitForSeconds(5f);
        text3d = GetComponent<TextMesh>();
        text3d.text = "use left hand to \n set floor height";
        
    }
}
