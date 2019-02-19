using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    public TextAsset DialogosJson;
    // Start is called before the first frame update
    void Start()
    {
        Game.Setup();

        try
        {
            string dataAsJson = DialogosJson.text;
            JsonUtility.FromJsonOverwrite(dataAsJson, Game.Dialogs);
        } catch (Exception e) {
            Debug.LogException(e);
        }
    }


}
