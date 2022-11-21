using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSaveController : MonoBehaviour
{
    string saveFile = "";

    private void Awake()
    {
        //saveFile = Application.persistentDataPath + "/Data.txt";
    }

    public string ReadFile()
    {
        var result = File.ReadAllText(saveFile);
        return result;
    }

    public void WriteFile(string jsonString, string fileName)
    {
        saveFile = Application.persistentDataPath + "/" + AddExtension(fileName);
        File.WriteAllText(saveFile, jsonString);
    }

    private string AddExtension(string filename)
    {
        return filename + ".txt";
    }
}
