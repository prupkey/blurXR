using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveAndLoader : MonoBehaviour
{
    public List<ObjectSaveData> saveListData = new List<ObjectSaveData>();

    public string savefile;
    public TextMesh testTEXT;
    public string saveData;


    private void Awake()
    {
        savefile = Application.persistentDataPath + "/room.json";
    }

    public void save()
    {
        saveData = convertToCSV();
        //File.WriteAllText(savefile, saveData);
    }

    private void Update()
    {
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
        {
            // saveListData = convertFromCSV();
            testTEXT.text = saveData;

        }
        if (OVRInput.GetUp(OVRInput.Button.Three))
        {
            save();
        }
    }


    // CSV CONVERSION


    public string convertToCSV()
    {
        string finalString = "";

        for (int i = 0; i < saveListData.Count; i++) //for every item in saved item list, it will sperate by commas and end with a line break
        {

            // position to csv

            finalString = finalString + saveListData[i].objPosition.x.ToString();

            finalString = finalString + ",";

            finalString = finalString + saveListData[i].objPosition.y.ToString();

            finalString = finalString + ",";

            finalString = finalString + saveListData[i].objPosition.z.ToString();

            finalString = finalString + ",";


            // scale to csv

            finalString = finalString + saveListData[i].objLocalScale.x.ToString();

            finalString = finalString + ",";

            finalString = finalString + saveListData[i].objLocalScale.y.ToString();

            finalString = finalString + ",";

            finalString = finalString + saveListData[i].objLocalScale.z.ToString();

            finalString = finalString + ",";


            // quat to csv

            finalString = finalString + saveListData[i].objQuat.x.ToString();

            finalString = finalString + ",";

            finalString = finalString + saveListData[i].objQuat.y.ToString();

            finalString = finalString + ",";

            finalString = finalString + saveListData[i].objQuat.z.ToString();

            finalString = finalString + ",";

            finalString = finalString + saveListData[i].objQuat.w.ToString();


            finalString = finalString + "\n";
        }
        return finalString;
    }


    public List<ObjectSaveData> convertFromCSV()
    {
        string fromFile = saveData;
        string[] lines = fromFile.Split('\n'); //splits string based on line breaks
        List<ObjectSaveData> saveList = new List<ObjectSaveData>(); // a new list that is the new save data
        for (int i = 0; i < lines.Length; i++) // goes through each item in list
        {
            string[] items = lines[i].Split(','); //splits string based on commas
            ObjectSaveData newData = new ObjectSaveData(); // new data
            newData.objPosition = new Vector3(float.Parse(items[0]), float.Parse(items[1]), float.Parse(items[2])); // writes the positions to the new data set 
            newData.objLocalScale = new Vector3(float.Parse(items[3]), float.Parse(items[4]), float.Parse(items[5])); // writes the local scale to the new data set 
            newData.objQuat = new Vector4(float.Parse(items[6]), float.Parse(items[7]), float.Parse(items[8]), float.Parse(items[9])); // writes the quaternion to the new data set 
            saveList.Add(newData); // adds new data to the save list 
        }

        return saveList; // returns a list of new save data
    }
}
