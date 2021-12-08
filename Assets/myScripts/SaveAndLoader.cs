using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveAndLoader : MonoBehaviour
{
    public List<ObjectSaveData> saveListData = new List<ObjectSaveData>();
    public string savefile;
    public TextMesh testTEXT;
    public string jsonDATA;


    private void Awake()
    {
        savefile = Application.persistentDataPath + "/room.json";

    }

    public void save()
    {
        jsonDATA = JsonUtility.ToJson(saveListData);
        File.WriteAllText(savefile, jsonDATA);

    }

    public void TESTload()
    {
        testTEXT.text = jsonDATA;
    }

    private void Update()
    {
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
        {
            TESTload();
        }

        if (OVRInput.GetUp(OVRInput.Button.Three))
        {
            save();
        }
    }


}
