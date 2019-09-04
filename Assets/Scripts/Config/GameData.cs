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
    public List<ClassAcao> Acoes;
    public List<ClassPersonagem> Personagens;
    public List<ClassDemanda> Demandas;
    public List<ClassResource> Recursos;
    public List<ClassAluno> Alunos;


    public int UrgenciaMinima;

    public GameData(GameManager gameManager)
    {
        LevelDemandingStudents = new[] {4, 12, 17};
        gameManager.StartCoroutine(LoadJson());
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
            UnityWebRequest www = UnityWebRequest.Get(jsonFile);
            yield return www.SendWebRequest();
            json = www.downloadHandler.text;
            JsonUtility.FromJsonOverwrite(json, this);
        }

        yield return null;
    }
}