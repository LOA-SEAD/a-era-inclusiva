using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager
{
    private readonly string _savePath = Path.Combine(Application.persistentDataPath, "saves");

    public SaveData Load(string name)
    {
        Debug.Log($"Loading from  {Path.Combine(_savePath, name + ".save")}");

        using (var streamReader = File.OpenText(Path.Combine(_savePath, name + ".save")))
        {
            var jsonString = streamReader.ReadToEnd();
            Debug.Log("Load complete!");

            return JsonUtility.FromJson<SaveData>(jsonString);
        }

    }

    public void Save(SaveData save)
    {
        Debug.Log($"Saving at {Path.Combine(_savePath, save.Name + ".save")}");
        if (!Directory.Exists(_savePath)) Directory.CreateDirectory(_savePath);
        var jsonString = JsonUtility.ToJson(save);

        using (var streamWriter = File.CreateText(Path.Combine(_savePath, save.Name + ".save")))
        {
            streamWriter.Write(jsonString);
        }
        Debug.Log("Save complete!");

    }

    public bool SaveExists(string name)
    {
        return File.Exists(Path.Combine(_savePath, name + ".save"));
    }

    public List<string> GetAllSaves()
    {
        return new List<string>();
    }
}