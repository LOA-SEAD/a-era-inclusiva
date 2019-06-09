using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource backgroundSource;
    public AudioSource source;
    public AudioSource foregroundSource;
    public AudioClip onClickClip;
    public AudioClip onHoverClip;
    public AudioClip backgroundClip;
    public AudioClip returnAudioClip;
    public AudioClip OnEnter;

    public float Volume
    {
        get { return source.volume; }
        set
        {
            source.volume = value;
            backgroundSource.volume = value;
        }
    }

    public void OnButtonClick()
    {
        Play(onClickClip);
    }

    public void OnButtonHover()
    {
        Play(onHoverClip);
    }

    public void OnReturn()
    {
        Play(returnAudioClip);
    }

    void PlayForeground(AudioClip audio)
    {
        foregroundSource.clip = audio;
        foregroundSource.Play();
    }

    void PlayBackground(AudioClip audio)
    {
        backgroundSource.clip = audio;
        backgroundSource.Play();
    }

    void Play(AudioClip audio)
    {
        source.clip = audio;
        source.Play();
    }

    void SetAndPlayBackground(bool loop)
    {
        PlayBackground(backgroundClip);
        backgroundSource.loop = loop;
    }


    private void Start()
    {
        if (backgroundClip != null)
            SetAndPlayBackground(true);

        if (OnEnter != null)
            PlayForeground(OnEnter);
    }


}