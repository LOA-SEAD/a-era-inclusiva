using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class GameData
{
    string filePath = Path.Combine(Application.streamingAssetsPath, "Json");

    public int[] LevelDemandingStudents;
    public List<ClassAcao> acoes;
    public List<ClassPersonagem> personagens;
    public List<ClassDemanda> demandas;
    public List<ClassResource> resources;
    public List<ClassAluno> alunos;


    public int UrgenciaMinima;

    public GameData()
    {
        LevelDemandingStudents = new[] {4, 12, 17};
        LoadJson();
    }


    public GameData(SaveData saveData)
    {
        UrgenciaMinima = 2;
        LevelDemandingStudents = new[] {4, 12, 17};
    }

    IEnumerator LoadJson()
    {
        string json;
        List<string> jsonToLoad = Directory.GetFiles(filePath).Where(x => Path.GetExtension(x) == ".json").ToList();
        foreach (string jsonFile in jsonToLoad)
        {

#if UNITY_ANDROID || UNITY_WEBGL
                UnityWebRequest www = UnityWebRequest.Get(filePath);
                yield return www.Send();
                json = www.downloadHandler.text;
#else
            json = File.ReadAllText(jsonFile);

#endif
            JsonUtility.FromJsonOverwrite(json, this);
        }
        return null;

    }
}