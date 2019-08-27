using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static  event EventHandler ConfigChanged;
    public static  event EventHandler PlayerDataChanged;

    private static ConfigData _configData;

    public static SoundManager SoundManager { get; private set; }

    public static SaveManager SaveManager { get; private set; }

    public static GraphicsManager GraphicsManager { get; private set; }

    public static GameData GameData { get; private set; }

    public static PlayerData PlayerData { get; private set; }

    public static ConfigData ConfigData
    {
        get { return _configData; }
        set
        {
            _configData = value;
            ConfigChanged?.Invoke(ConfigData, EventArgs.Empty);

        }
    }

    public void Awake()
    {
        if (SaveManager == null) SaveManager = new SaveManager();
        if (GameData == null) GameData = new GameData();


        if (ConfigData == null)
        {
            if (SaveManager.ConfigExists())
                ConfigData = SaveManager.LoadConfig();
            else 
                ConfigData = new ConfigData();
            
            GraphicsManager = new GraphicsManager(ConfigData); 
            SoundManager = new SoundManager(ConfigData);
            ConfigChanged += GraphicsManager.Load;
            ConfigChanged += SoundManager.Load;
            SaveManager.SaveConfig(ConfigData);
        }
       

      
    }

    public static void Load(string name)
    {
        var saveData = SaveManager.Load(name);
        if (PlayerData == null) PlayerData = new PlayerData(saveData);
    }

    public static void New(string name)
    {
        if (PlayerData == null) PlayerData = new PlayerData();
        if (SaveManager == null) SaveManager = new SaveManager();
        var saveData = new SaveData(name, GraphicsManager, SoundManager, PlayerData);
        SaveManager.Save(saveData);
    }



}