using UnityEngine;

public class VoiceTest : MonoBehaviour
{
    private void Start()
    {
        VoiceManager.Instance.PlayVoice("Audio/Dialogos/Andre/andre_0.mp3");
    }
}
