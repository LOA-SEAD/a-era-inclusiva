using UnityEngine;
using UnityEngine.UI;

public class BalaoNotificacao : MonoBehaviour
{
    public Image image;
    public Sprite[] SpriteEachLevel;

    public int Level
    {
        set => image.sprite = SpriteEachLevel[value];
    }

    private void Start()
    {
        Destroy(gameObject, 6f);
    }
}