using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    public TextAsset JsonMethodologies;
    public TextAsset JsonStudents;
    public TextAsset JsonDialogs;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            JsonUtility.FromJsonOverwrite(JsonMethodologies.text, Game.Methodologies);
            Game.Students = JsonUtility.FromJson<List<ClassAlunos>>(JsonStudents.text);
            Game.Dialogs = JsonUtility.FromJson<List<ClassFalas>>(JsonDialogs.text);
        } catch (Exception e)
        {
            Debug.LogError(e);
        }
    }


}
