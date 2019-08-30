using System;
using UnityEngine;

public class SoundManager
{
    private float _background;
    private float _effects;

    public float Background
    {
        get => _background;
        set
        {
            _background = value;
            var audioSources = GameObject.FindGameObjectsWithTag("BackgroundAudioSource");
            foreach (var audioSource in audioSources)
            {
                audioSource.GetComponent<AudioSource>().volume = _background;
            }
        }
    }

    public float Effects
    {
        get => _effects;
        set
        {
            _effects = value;
            var audioSources = GameObject.FindGameObjectsWithTag("EffectAudioSource");
            foreach (var audioSource in audioSources)
            {
                audioSource.GetComponent<AudioSource>().volume = _effects;
            }
        }
    }



    public SoundManager()
    {
        this.Background = 100f;
        this.Effects = 100f;
    }

    public SoundManager(ConfigData data)
    {
        this.Background = data.BackgroundVol;
        this.Effects = data.EffectsVol;
    }

    public void Load(object obj, EventArgs e)
    {
        var data = (ConfigData) obj;
        this.Background = data.BackgroundVol;
        this.Effects = data.EffectsVol;
    }
}