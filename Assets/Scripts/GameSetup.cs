using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    public TextAsset JsonActions;
    public TextAsset JsonStudents;
    public TextAsset JsonDialogs;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            JsonUtility.FromJsonOverwrite(JsonActions.text, Game.Actions);
            Game.Students = JsonUtility.FromJson<List<ClassAluno>>(JsonStudents.text);
            Game.Dialogs = JsonUtility.FromJson<List<ClassFala>>(JsonDialogs.text);
        } catch (Exception e)
        {
            Debug.LogError(e);
        }
    }


}
