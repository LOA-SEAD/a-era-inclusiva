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
        get => _soundData.ambienceVol;
        set
        {
            _soundData.ambienceVol = value;
            var audioSources = GameObject.FindGameObjectsWithTag("BackgroundAudioSource");
            foreach (var audioSource in audioSources) audioSource.GetComponent<AudioSource>().volume = _soundData.ambienceVol;
            Save();
        }
    }

    public float Effects
    {
        get => _soundData.effectsVol;
        set
        {
            _soundData.effectsVol = value;
            var audioSources = GameObject.FindGameObjectsWithTag("EffectAudioSource");
            foreach (var audioSource in audioSources) audioSource.GetComponent<AudioSource>().volume = _soundData.effectsVol;
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