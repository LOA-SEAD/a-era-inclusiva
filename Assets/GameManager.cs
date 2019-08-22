using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static SoundManager _soundManager;

    private static SaveManager _saveManager;

    private static GraphicsManager _graphicsManager;
    
    private static GameData _gameData;
    private static PlayerData _playerData;

    public static SoundManager SoundManager => _soundManager;

    public static SaveManager SaveManager => _saveManager;

    public static GraphicsManager GraphicsManager => _graphicsManager;
    
    public static GameData GameData => _gameData;
    public static PlayerData PlayerData=> _playerData;


    public void Awake()
    {
        if (_gameData == null)
        {
            _gameData = new GameData();
        }  
    }
     
    public void Load(string name)
    {
        if (_saveManager == null)
        {
            _saveManager = new SaveManager();
        }
        var saveData = _saveManager.Load(name); 
        if (_soundManager == null)
        {
            _soundManager = new SoundManager(saveData);
        }
        if (_playerData == null)
        {
            _playerData = new PlayerData(saveData);
        }  
        if (_graphicsManager == null)
        {
            _graphicsManager = new GraphicsManager(saveData);
        }  
    }

    public void New(string name)
    {
        Debug.Log(name);
        if (_soundManager == null)
        {
            _soundManager = new SoundManager();
        }
    
        if (_graphicsManager == null)
        {
            _graphicsManager = new GraphicsManager();
        }  
        if (_playerData == null)
        {
            _playerData = new PlayerData();
        }  
        if (_saveManager == null)
        {
            _saveManager = new SaveManager();
        }
        var saveData = new SaveData(name, GraphicsManager,SoundManager,PlayerData);
        SaveManager.Save(saveData);
    }
    
}
