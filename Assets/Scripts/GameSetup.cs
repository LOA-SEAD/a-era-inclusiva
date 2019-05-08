using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    public TextAsset DialogosJson;
    public TextAsset AcoesJson;
    public TextAsset AlunosJson;
    public TextAsset DemandasJson;
    public TextAsset ResourcesJson;

    // Start is called before the first frame update
    void Start()
    {
        Game.Setup();
        try
        {
            JsonUtility.FromJsonOverwrite(DialogosJson.text, Game.Characters);
            JsonUtility.FromJsonOverwrite(AcoesJson.text, Game.Actions);
            JsonUtility.FromJsonOverwrite(AlunosJson.text, Game.Students);
            JsonUtility.FromJsonOverwrite(DemandasJson.text, Game.Demands);
            JsonUtility.FromJsonOverwrite(ResourcesJson.text, Game.Resources);

        }
        catch (Exception e) {
            Debug.LogException(e);
        }
    }


}
