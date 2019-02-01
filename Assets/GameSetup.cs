using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    public TextAsset JsonMetodologias;
    public TextAsset JsonAlunos;
    public TextAsset JsonFalas;
    // Start is called before the first frame update
    void Start()
    {
        try
        {

            JsonUtility.FromJsonOverwrite(JsonMetodologias.text, Game.Metodologias);
       
            Game.Alunos = JsonUtility.FromJson<List<ClassAlunos>>(JsonAlunos.text);
            Game.Falas = JsonUtility.FromJson<List<ClassFalas>>(JsonFalas.text);
        } catch (Exception e)
        {
            Debug.LogError(e);
        }
    }


}
