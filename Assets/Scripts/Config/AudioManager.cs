using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    // Singleton
    public static AudioManager instance;

    // 3 audio sources for each type of sound in the game
    public static AudioSource effects, ambience, music;

    // Array of audio clips
    public Sound[] sounds;

    // Control of FadeIn(), FadeOut() functions
    private static bool keepFadingIn, keepFadingOut;

    // POSSIVELMENTE FAZER UM AUDIO SOURCE POR CLIP, PRA EVITAR QUE O SOM SEJA CORTADO
    // Não esquecer de mudar as configurações de compressão pra cada clipe
    // Setar um script nos prefabs em que existe AudioSource para que chamem o AudioManager quando forem tocar algo

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
            effects.PlayOneShot(sound.clip);
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

    public void StopSfx()
    {
        effects.Stop();
    }

    public void StopAmbience()
    {
        ambience.Stop();
    }

    public void StopMusic()
    {
        music.Stop();
    }

    public void ChangeSfxVolume(float value)
    {
        effects.volume = value;
    }

    public void ChangeAmbVolume(float value)
    {
        ambience.volume = value;
    }

    public void ChangeMusVolume(float value)
    {
        music.volume = value;
    }

    public static void FadeInAmbience(int soundType, float speed)
    {
        instance.StartCoroutine(FadeIn(ambience, soundType, speed, ambience.volume));
    }

    public static void FadeOutAmbience(int soundType, float speed)
    {
        instance.StartCoroutine(FadeOut(ambience, soundType, speed));
    }

    static IEnumerator FadeIn(AudioSource audioSource, int soundType, float speed, float maxVolume)
    {
        keepFadingIn = true;
        keepFadingOut = false;

        audioSource.volume = 0;

        while(audioSource.volume < maxVolume && keepFadingIn)
        {
            audioSource.volume += speed;
            yield return new WaitForSeconds(0.1f);
        }
    }

    static IEnumerator FadeOut(AudioSource audioSource, int soundType, float speed)
    {
        keepFadingIn = false;
        keepFadingOut = true;

        while (audioSource.volume >= 0 && keepFadingOut)
        {
            audioSource.volume -= speed;
            yield return new WaitForSeconds(0.1f);
        }
    }
}