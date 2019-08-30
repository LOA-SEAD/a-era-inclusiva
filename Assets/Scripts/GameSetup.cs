using System;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    public TextAsset AcoesJson;
    public TextAsset AlunosJson;
    public TextAsset DemandasJson;
    public TextAsset DialogosJson;
    public TextAsset ResourcesJson;

    // Start is called before the first frame update
    public void Start()
    {
        return;
        try
        {
            JsonUtility.FromJsonOverwrite(DialogosJson.text, GameManager.GameData);
            JsonUtility.FromJsonOverwrite(AcoesJson.text, GameManager.GameData);
            JsonUtility.FromJsonOverwrite(AlunosJson.text, GameManager.GameData);
            JsonUtility.FromJsonOverwrite(DemandasJson.text, GameManager.GameData);
            JsonUtility.FromJsonOverwrite(ResourcesJson.text, GameManager.GameData);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }
}