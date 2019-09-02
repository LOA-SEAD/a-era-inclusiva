using System.IO;
using UnityEngine;

public class SoundManager
{
    private readonly string _configFile = Path.Combine(Application.persistentDataPath, "saves", "audio_settings.json");
    private readonly string _savePath = Path.Combine(Application.persistentDataPath, "saves");
    private SoundData _soundData;


    public SoundManager()
    {
        if (ConfigExists())
        {
            Load();
        }
        else
        {
            Background = 1f;
            Effects = 1f;
            Save();
        }
    }


    public float Background
    {
        get => _soundData.BackgroundVol;
        set
        {
            _soundData.BackgroundVol = value;
            var audioSources = GameObject.FindGameObjectsWithTag("BackgroundAudioSource");
            foreach (var audioSource in audioSources) audioSource.GetComponent<AudioSource>().volume = _soundData.BackgroundVol;
            Save();
        }
    }

    public float Effects
    {
        get => _soundData.EffectsVol;
        set
        {
            _soundData.EffectsVol = value;
            var audioSources = GameObject.FindGameObjectsWithTag("EffectAudioSource");
            foreach (var audioSource in audioSources) audioSource.GetComponent<AudioSource>().volume = _soundData.EffectsVol;
            Save();
        }
    }

    public bool ConfigExists()
    {
        if (!Directory.Exists(_savePath)) Directory.CreateDirectory(_savePath);
        return File.Exists(_configFile);
    }

    public void Load()
    {
        using (var streamReader = File.OpenText(_configFile))
        {
            var jsonString = streamReader.ReadToEnd();
            _soundData = JsonUtility.FromJson<SoundData>(jsonString);
        }
    }

    public void Save()
    {
        var jsonString = JsonUtility.ToJson(_soundData);

        using (var streamWriter = File.CreateText(_configFile))
        {
            streamWriter.Write(jsonString);
        }
    }
}