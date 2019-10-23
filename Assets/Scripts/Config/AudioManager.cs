using System.Collections;
using System.IO;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Singleton
    public static AudioManager instance;

    // 3 audio sources for each type of sound in the game
    public AudioSource effects, ambience, music;

    // For volume configuration
    private SoundData _soundData;

    // Array of audio clips
    public Sound[] sounds;

    // Control of FadeIn(), FadeOut() functions
    private static bool keepFadingIn, keepFadingOut;

    // For saving configuration
    private static string _configFile;
    private static string _savePath;

    // TODO: POSSIVELMENTE FAZER UM AUDIO SOURCE POR CLIP, PRA EVITAR QUE O SOM SEJA CORTADO
    // TODO: Modificar o script do prefab Dialog para utilizar o AudioManager
    // TODO: Não esquecer de mudar as configurações de compressão pra cada clipe
    // TODO: Setar um script nos prefabs em que existe AudioSource para que chamem o AudioManager quando forem tocar algo
    // TODO: AudioSources privados e manter uma propriedade pra acessá-los. Não inicializar pelo inspetor, mas sim por esse script
    // TODO: Mover as coisas de Save pro SaveManager  
    // TODO: mandar save() da configuração ao clicar em Voltar (ConfigPanel)
    // TODO: Suportar som stereo?
    // TODO: Para sons muito repetitivos (como clique e hover de botão) modificar o pitch levemente para não ficar cansativo

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            _savePath = Path.Combine(Application.persistentDataPath, "saves");
            _configFile = Path.Combine(Application.persistentDataPath, "saves", "audio_settings.json");

            if (ConfigExists())
            {
                Load();
            }
            else
            {
                effects.volume = 0.9f;
                ambience.volume = 0.5f;
                Save();
            }
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

        effects.volume =_soundData.effectsVol;
        ambience.volume = _soundData.ambienceVol;
    }

    public void Save()
    {
        _soundData.effectsVol = effects.volume;
        _soundData.ambienceVol = ambience.volume;

        var jsonString = JsonUtility.ToJson(_soundData);

        using (var streamWriter = File.CreateText(_configFile))
        {
            streamWriter.Write(jsonString);
        }
    }

    //------------------------------------------------------------------

    public Sound GetSound(SoundType soundType)
    {
        foreach(Sound sound in sounds)
            if (sound.soundType == soundType) return sound;

        Debug.Log("Sound '" + name + "' not found.");
        return null;
    }

    public void PlaySfx(int soundType)
    {
        Sound sound = GetSound((SoundType) soundType);

        if (sound != null)
        {
            if (sound.clip == effects.clip)
            {
                effects.Stop();
                effects.Play();
            } else
            {
                effects.clip = sound.clip;
                effects.PlayOneShot(sound.clip);
            }
        }
    }

    public void PlayAmbience(int soundType)
    {
        Sound sound = GetSound((SoundType)soundType);

        if (sound != null)
        {
            ambience.clip = sound.clip;
            ambience.Play();
        }
    }

    public void PlayMusic(int soundType)
    {
        Sound sound = GetSound((SoundType)soundType);

        if (sound != null)
        {
            music.clip = sound.clip;
            music.Play();
        }
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