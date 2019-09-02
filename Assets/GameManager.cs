using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static SoundData soundData;

    public static bool IsLoaded;
    public static bool AccessibilityMode;
    public static SoundManager SoundManager { get; private set; }

    public static SaveManager SaveManager { get; private set; }


    public static GameData GameData { get; private set; }

    public static PlayerData PlayerData { get; private set; }


    public void Awake()
    {
        if (IsLoaded) return;

        GameData = new GameData();
        SaveManager = new SaveManager();
        SoundManager = new SoundManager();
        IsLoaded = true;
    }


    public static void Load(string name)
    {
        var saveData = SaveManager.Load(name);
        if (PlayerData == null) PlayerData = new PlayerData(saveData);
    }

    public static void New(string name)
    {
        if (PlayerData == null) PlayerData = new PlayerData();
        var saveData = new SaveData(name, SoundManager, PlayerData);
        SaveManager.Save(saveData);
    }
}