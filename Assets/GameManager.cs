using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static SoundManager SoundManager { get; private set; }

    public static SaveManager SaveManager { get; private set; }

    public static GraphicsManager GraphicsManager { get; private set; }

    public static GameData GameData { get; private set; }

    public static PlayerData PlayerData { get; private set; }


    public void Awake()
    {
        if (GameData == null) GameData = new GameData();
        if (GraphicsManager == null) GraphicsManager = new GraphicsManager();
        if (SoundManager == null) SoundManager = new SoundManager();

    }

    public static void Load(string name)
    {
        if (SaveManager == null) SaveManager = new SaveManager();
        var saveData = SaveManager.Load(name);
        if (SoundManager == null) SoundManager = new SoundManager(saveData);
        if (PlayerData == null) PlayerData = new PlayerData(saveData);
        if (GraphicsManager == null) GraphicsManager = new GraphicsManager(saveData);
    }

    public static void New(string name)
    {
        if (PlayerData == null) PlayerData = new PlayerData();
        if (SaveManager == null) SaveManager = new SaveManager();
        var saveData = new SaveData(name, GraphicsManager, SoundManager, PlayerData);
        SaveManager.Save(saveData);
    }
}