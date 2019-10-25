using UnityEngine;

[System.Serializable]
public class Sound
{
    public SoundType soundType;
    public AudioClip clip;
}

[System.Serializable]
public class SFX : Sound
{
    [HideInInspector]
    public AudioSource source;
}