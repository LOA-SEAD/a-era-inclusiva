using System.Collections.Generic;
using System.Collections;
using System.IO;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // TODO: POSSIVELMENTE FAZER UM AUDIO SOURCE POR CLIP, PRA EVITAR QUE O SOM SEJA CORTADO
    // TODO: Modificar o script do prefab Dialog para utilizar o AudioManager
    // TODO: Não esquecer de mudar as configurações de compressão pra cada clipe
    // TODO: Setar um script nos prefabs em que existe AudioSource para que chamem o AudioManager quando forem tocar algo
    // TODO: AudioSources privados e manter uma propriedade pra acessá-los. Não inicializar pelo inspetor, mas sim por esse script
    // TODO: Mover as coisas de Save pro SaveManager  
    // TODO: mandar save() da configuração ao clicar em Voltar (ConfigPanel)
    // TODO: Suportar som stereo?
    // TODO: Para sons muito repetitivos (como clique e hover de botão) modificar o pitch levemente para não ficar cansativo
    // TODO: Talvez ficar acessando Screen.width e Screen.height toda vez que for tocar um som seja custoso?
    // TODO: Fazer uma nova cena para testar se as fórmulas de pitch e stereo ao tocar um sfx estão certas

    // Singleton
    public static AudioManager instance;
    const float pitchMax = 1.1f;
    const float pitchMin = 0.90f;

    private AudioSource ambience;
    public float AmbienceVolume {
        get
        {
            return ambience.volume;
        }
        set
        {
            ambience.volume = value;
        }
    }

    private AudioSource music;
    public float MusicVolume
    {
        get
        {
            return music.volume;
        }
        set
        {
            music.volume = value;
        }
    }

    private SoundData _soundData;

    public SFX[] fxSounds;
    public float SFXVolume
    {
        get
        {
            return fxSounds[0].source.volume;
        }
        set
        {
            foreach (SFX sfx in fxSounds)
                sfx.source.volume = value;
        }
    }

    public Sound[] ambientSounds;
    public Sound[] musicSounds;

    private static bool keepFadingIn, keepFadingOut;

    private static string _configFile;
    private static string _savePath;

    public void Initialize()
    {
        ambience = gameObject.AddComponent<AudioSource>();
        ambience.loop = true;
        music = gameObject.AddComponent<AudioSource>();
        music.loop = true;
        foreach (SFX effect in fxSounds)
        {
            effect.source = gameObject.AddComponent<AudioSource>();
            effect.source.clip = effect.clip;
        }
        _savePath = Path.Combine(Application.persistentDataPath,Application.version, "saves");
        _configFile = Path.Combine(_savePath, "audio_settings.json");
        Debug.Log(_configFile);

        if (ConfigExists())
        {
            Load();
        }
        else
        {
            SFXVolume = 0.7f;
            AmbienceVolume = 0.4f;
            MusicVolume = 0.6f;
            Save();
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            Initialize();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //-----------------------SAVING FUNCTIONS---------------------------

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

        SFXVolume =_soundData.effectsVol;
        AmbienceVolume = _soundData.ambienceVol;
        MusicVolume = _soundData.musicVol;
    }

    public void Save()
    {
        _soundData.effectsVol = SFXVolume;
        _soundData.ambienceVol = AmbienceVolume;
        _soundData.musicVol = MusicVolume;

        var jsonString = JsonUtility.ToJson(_soundData);

        using (var streamWriter = File.CreateText(_configFile))
        {
            streamWriter.Write(jsonString);
        }
    }

    //------------------------------------------------------------------

    public SFX GetSFX(SoundType soundType)
    {
        foreach(SFX sfx in fxSounds)
            if (sfx.soundType == soundType) return sfx;

        Debug.Log("SFX '" + soundType + "' not found.");
        return null;
    }

    public Sound GetAmbience(SoundType soundType)
    {
        foreach (Sound sound in ambientSounds)
            if (sound.soundType == soundType) return sound;

        Debug.Log("Ambience '" + soundType + "' not found.");
        return null;
    }

    public Sound GetMusic(SoundType soundType)
    {
        foreach (Sound sound in musicSounds)
            if (sound.soundType == soundType) return sound;

        Debug.Log("Music '" + soundType + "' not found.");
        return null;
    }

    // Função altera stereo com posx e pitch de acordo com posy
    public void PlaySfx(int soundType, float posx, float posy)
    {
        SFX sfx = GetSFX((SoundType) soundType);

        if (sfx != null)
        {
            sfx.source.Stop();

            float halfw = Screen.width / 2f;
            float height = Screen.height;

            sfx.source.panStereo = (posx - halfw)/ halfw;
            sfx.source.pitch = ((posy * (pitchMax - pitchMin)) / height) + pitchMin;
            sfx.source.Play();
        }
    }

    // Caso chame a função só passando posx, só muda stereo, com pitch normal
    public void PlaySfx(int soundType, float posx)
    {
        PlaySfx(soundType, posx, Screen.height / 2);
    }

    // Caso chame a função sem passar pos, stereo e pitch normais
    public void PlaySfx(int soundType)
    {
        PlaySfx(soundType, Screen.width / 2, Screen.height / 2);
    }


    public void PlayAmbience(int soundType)
    {
        Sound sound = GetAmbience((SoundType)soundType);

        if (sound != null && (sound.clip != music.clip || !music.isPlaying))
        {
            ambience.clip = sound.clip;
            ambience.Play();
        }
    }

    public void PlayMusic(int soundType)
    {
        Sound sound = GetMusic((SoundType)soundType);

        if (sound != null && (sound.clip != music.clip || !music.isPlaying))
        {
            music.clip = sound.clip;
            music.Play();
        }
    }

    public void StopAmbience()
    {
        ambience.Stop();
    }

    public void StopMusic()
    {
        music.Stop();
    }

    public static void FadeInAmbience(int soundType, float speed)
    {
        instance.PlayAmbience(soundType);
        instance.StartCoroutine(FadeIn(instance.ambience, speed, instance.ambience.volume));
    }

    public static void FadeOutAmbience(float speed)
    {
        if(instance.ambience.isPlaying)
            instance.StartCoroutine(FadeOut(instance.ambience, speed));
    }

    static IEnumerator FadeIn(AudioSource audioSource, float speed, float maxVolume)
    {
        keepFadingIn = true;
        keepFadingOut = false;

        audioSource.volume = 0f;
        float adjustedVolume = 0.2f;

        while (audioSource.volume < maxVolume && keepFadingIn)
        {
            adjustedVolume += adjustedVolume * Time.deltaTime / speed;
            audioSource.volume = adjustedVolume;
            yield return new WaitForSeconds(0.1f);
        }
    }

    static IEnumerator FadeOut(AudioSource audioSource, float speed)
    {
        keepFadingIn = false;
        keepFadingOut = true;

        float startVolume = audioSource.volume;
        float adjustedVolume = startVolume;

        while (audioSource.volume > 0 && keepFadingOut)
        {
            adjustedVolume -= startVolume * Time.deltaTime / speed;
            audioSource.volume = adjustedVolume;
            yield return new WaitForSeconds(0.1f);
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
}