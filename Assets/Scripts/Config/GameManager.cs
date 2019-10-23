using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static SoundData soundData;
    
    public static bool IsLoaded;
    public static bool AccessibilityMode;

    public static SaveManager SaveManager { get; private set; }

    public static GameData GameData { get; private set; }

    public static PlayerData PlayerData { get; set; }

    public void Awake()
    {
        if (IsLoaded) return;
        
        GameData = new GameData(this);
        SaveManager = new SaveManager();
        if (SaveManager.SaveExists("save")) 
            SaveManager.Load("save");
        else New("save");
        IsLoaded = true;


    }

    public static void New(string name)
    {
        if (PlayerData == null) PlayerData = new PlayerData();
        var saveData = new SaveData(name, PlayerData);
        SaveManager.Save(saveData);
    }
}