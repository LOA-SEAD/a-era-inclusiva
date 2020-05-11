using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class GameData
{
    string filePath ="Json";

    public int[] LevelDemandingStudents;
    public List<ClassAcao> Acoes;
    public List<ClassPersonagem> Personagens;
    public List<ClassDemanda> Demandas;
    public List<ClassResource> Recursos;
    public List<ClassAluno> Alunos;
    public List<ClassResourceRM> RecursosRM;
    public List<ClassAula> Aulas;

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
        var jsonToLoad = BetterStreamingAssets.GetFiles(filePath).Where(x => Path.GetExtension(x) == ".json").ToList();
        while (jsonToLoad.Count != 0)
        {
            var jsonFile = jsonToLoad[0];
            jsonToLoad.RemoveAt(0);
            var jsonText = BetterStreamingAssets.ReadAllText(jsonFile);
            try
            {
                JsonUtility.FromJsonOverwrite(jsonText, this);
            }
            catch (Exception e)
            {
                Debug.Log($"Falha ao carreggar {jsonFile}, erro: {e.Message}");
            }

            yield return 0;
        }

        Loaded = true;
        GameDataLoaded?.Invoke(this, EventArgs.Empty);


    }
}