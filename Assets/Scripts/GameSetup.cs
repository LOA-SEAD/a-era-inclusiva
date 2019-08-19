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
    public GameManager gameManager;
  
    // Start is called before the first frame update
    public void Setup()
    {
   
        try
        {
            JsonUtility.FromJsonOverwrite(DialogosJson.text, GameManager.GameData.Characters);
            JsonUtility.FromJsonOverwrite(AcoesJson.text, GameManager.GameData.Actions);
            JsonUtility.FromJsonOverwrite(AlunosJson.text, GameManager.GameData.Students);
            JsonUtility.FromJsonOverwrite(DemandasJson.text, GameManager.GameData.Demands);
            JsonUtility.FromJsonOverwrite(ResourcesJson.text, GameManager.GameData.Resources);

        }
        catch (Exception e) {
            Debug.LogException(e);
        }
    }


}
