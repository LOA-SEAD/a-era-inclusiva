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
    public List<ClassResourceRM> RecursosRM;
    public bool Loaded = false;
    public static event EventHandler GameDataLoaded;

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
        while (jsonToLoad.Count != 0)
        {
            var jsonFile = jsonToLoad[0];
            jsonToLoad.RemoveAt(0);
            UnityWebRequest www = UnityWebRequest.Get("file://"+jsonFile);
            yield return www.SendWebRequest();
            json = www.downloadHandler.text;
            try
            {
                JsonUtility.FromJsonOverwrite(json, this);
            }
            catch (Exception e)
            {
                Debug.Log($"Falha ao carreggar {jsonFile}, erro: {e.Message}");
            }

            yield return 0;
        }
       
        yield return new WaitUntil(() => jsonToLoad.Count == 0);

        Loaded = true;
        GameDataLoaded?.Invoke(this, EventArgs.Empty);


    }
}