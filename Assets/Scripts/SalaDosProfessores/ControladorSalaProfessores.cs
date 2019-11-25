using UnityEngine;
using UnityEngine.UI;

public class ControladorSalaProfessores : MonoBehaviour
{
    public GameObject avatar;

    void Start()
    {
        PlayMusic();
    }

    public void PlayMusic()
    {
        AudioManager.instance.PlayMusic((int)SoundType.MusicRoom);
        avatar.GetComponent<Image>().sprite = GameManager.GetAvatarImage();
    }
    
    
}
