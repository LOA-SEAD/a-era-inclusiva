using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SaveManager
{
    private readonly string _savePath = Path.Combine(Application.persistentDataPath, "saves");
    private readonly string _configFile = Path.Combine(Application.persistentDataPath, "saves","config.json");

    public SaveData Load(string name)
    {
       
        using (StreamReader streamReader = File.OpenText(Path.Combine(_savePath, name+".save")))
        {
            string jsonString = streamReader.ReadToEnd();
            return JsonUtility.FromJson<SaveData>(jsonString);
        }
    }

    public void Save(SaveData save)
    {
        
        if(!Directory.Exists(_savePath))
        {
            Directory.CreateDirectory(_savePath);
        }
        string jsonString = JsonUtility.ToJson(save);

        using (StreamWriter streamWriter = File.CreateText(Path.Combine(_savePath, save.Name+".save")))
        {
            streamWriter.Write(jsonString);
        }
    }
    
    public bool SaveExists(string name)
    {
        return File.Exists(Path.Combine(_savePath, name + ".save"));
    }
    public List<string> GetAllSaves()
    {
        return new List<string>();
    }

    public bool ConfigExists()
    {
        return File.Exists(_configFile);
    }
    public ConfigData LoadConfig()
    {
       
        using (StreamReader streamReader = File.OpenText(_configFile))
        {
            string jsonString = streamReader.ReadToEnd();
            return JsonUtility.FromJson<ConfigData>(jsonString);
        }
    }

    public void SaveConfig(ConfigData save)
    {
        
        if(!Directory.Exists(_savePath))
        {
            Directory.CreateDirectory(_savePath);
        }
        string jsonString = JsonUtility.ToJson(save);

        using (StreamWriter streamWriter = File.CreateText(_configFile))
        {
            streamWriter.Write(jsonString);
        }
    }
}