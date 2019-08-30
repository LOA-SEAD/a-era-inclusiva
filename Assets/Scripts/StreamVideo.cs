using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StreamVideo : MonoBehaviour
{
    public AudioSource audioSource;
    public RawImage rawImage;
    public VideoPlayer videoPlayer;

// Use this for initialization
    public void Play()
    {
        StartCoroutine(PlayVideo());
    }

    public void SetSource(string src)
    {
        videoPlayer.Stop();
        videoPlayer.url = src;
    }

    private IEnumerator PlayVideo()
    {
        videoPlayer.Prepare();
        var waitForSeconds = new WaitForSeconds(1);
        while (!videoPlayer.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }

        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();
        audioSource.Play();
    }
}