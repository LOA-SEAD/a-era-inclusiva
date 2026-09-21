using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class VoiceManager : MonoBehaviour
{
    public static VoiceManager Instance { get; private set; }

    [Range(0f, 1f)]
    public float voiceVolume = 0.35f;

    public float VoiceVolume
    {
        get { return voiceVolume; }
    }

    private AudioSource voiceSource;
    private Dictionary<string, AudioClip> audioCache = new Dictionary<string, AudioClip>();
    private Coroutine currentVoiceRoutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Carrega o volume salvo. Se não existir, usa o valor do Inspector.
            voiceVolume = PlayerPrefs.GetFloat("voice_volume", voiceVolume);

            voiceSource = gameObject.AddComponent<AudioSource>();
            voiceSource.loop = false;
            voiceSource.playOnAwake = false;
            voiceSource.volume = voiceVolume;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayVoice(string audioSrc)
    {
        if (string.IsNullOrEmpty(audioSrc))
        {
            Debug.LogWarning("PlayVoice chamado sem caminho de áudio.");
            return;
        }

        StopVoice();

        currentVoiceRoutine = StartCoroutine(PlayVoiceSequenceRoutine(audioSrc));
    }

    public void StopVoice()
    {
        if (currentVoiceRoutine != null)
        {
            StopCoroutine(currentVoiceRoutine);
            currentVoiceRoutine = null;
        }

        if (voiceSource != null && voiceSource.isPlaying)
        {
            voiceSource.Stop();
        }
    }

    public void SetVolume(float volume)
    {
        voiceVolume = Mathf.Clamp01(volume);

        if (voiceSource != null)
        {
            voiceSource.volume = voiceVolume;
        }

        PlayerPrefs.SetFloat("voice_volume", voiceVolume);
        PlayerPrefs.Save();

        Debug.Log("Volume das vozes salvo: " + voiceVolume);
    }

    private IEnumerator PlayVoiceSequenceRoutine(string audioSrc)
    {
        string[] audioList = audioSrc.Split('|');

        foreach (string audio in audioList)
        {
            string cleanAudio = audio.Trim();

            if (string.IsNullOrEmpty(cleanAudio))
            {
                continue;
            }

            yield return StartCoroutine(PlaySingleVoiceRoutine(cleanAudio));

            while (voiceSource != null && voiceSource.isPlaying)
            {
                yield return null;
            }
        }

        currentVoiceRoutine = null;
    }

    private IEnumerator PlaySingleVoiceRoutine(string audioSrc)
    {
        if (audioCache.ContainsKey(audioSrc))
        {
            voiceSource.clip = audioCache[audioSrc];
            voiceSource.volume = voiceVolume;
            voiceSource.Play();

            Debug.Log("Tocando áudio em cache: " + audioSrc);
            yield break;
        }

        string url = BuildAudioUrl(audioSrc);
        AudioType audioType = GetAudioType(audioSrc);

        Debug.Log("Tentando carregar áudio em: " + url);

        using (UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(url, audioType))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogWarning("Erro ao carregar áudio: " + audioSrc + " | " + request.error);
                yield break;
            }

            AudioClip clip = DownloadHandlerAudioClip.GetContent(request);

            if (clip == null)
            {
                Debug.LogWarning("Áudio carregado como null: " + audioSrc);
                yield break;
            }

            audioCache[audioSrc] = clip;

            voiceSource.clip = clip;
            voiceSource.volume = voiceVolume;
            voiceSource.Play();

            Debug.Log("Tocando áudio: " + audioSrc);
        }
    }

    private string BuildAudioUrl(string audioSrc)
    {
        if (audioSrc.StartsWith("http://") || audioSrc.StartsWith("https://"))
        {
            return audioSrc;
        }

        string cleanPath = audioSrc.TrimStart('/', '\\');
        string fullPath = Path.Combine(Application.streamingAssetsPath, cleanPath);

#if UNITY_ANDROID && !UNITY_EDITOR
        return fullPath;
#else
        return "file://" + fullPath;
#endif
    }

    private AudioType GetAudioType(string audioSrc)
    {
        string extension = Path.GetExtension(audioSrc).ToLower();

        switch (extension)
        {
            case ".wav":
                return AudioType.WAV;

            case ".mp3":
                return AudioType.MPEG;

            case ".ogg":
                return AudioType.OGGVORBIS;

            default:
                return AudioType.UNKNOWN;
        }
    }
}