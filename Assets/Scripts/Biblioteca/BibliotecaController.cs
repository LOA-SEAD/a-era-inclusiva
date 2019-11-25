using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class BibliotecaController : MonoBehaviour
{
    public Image image;
    public GameObject Media;
    public StreamVideo player;
    public GameObject textoPages;
    public GameObject avatar;

    public void Start()
    {
        AudioManager.instance.PlayAmbience((int)SoundType.AmbienceHallway);
        AudioManager.instance.PlayMusic((int)SoundType.MusicRoom);
       
        avatar.GetComponent<Image>().sprite = GameManager.GetAvatarImage();
    }

    public void Display(ClassResource resource)
    {

        switch (resource.type)
        {
            case "video":
                Media.SetActive(true);

                player.gameObject.SetActive(true);
                Debug.Log("Carregando video de:" + Application.streamingAssetsPath + resource.src);
                player.SetSource(Application.streamingAssetsPath + resource.src);
                player.Play();
                break;
            case "image":
                Media.SetActive(true);

                image.gameObject.SetActive(true);
                Debug.Log("Carregando imagem de:" + Application.streamingAssetsPath + resource.src);
                StartCoroutine(LoadTextureInto(Application.streamingAssetsPath + resource.src, image));
                break;
            case "text":
                Media.SetActive(true);

                textoPages.SetActive(true);
                Debug.Log("Carregando texto de:" + Application.streamingAssetsPath + resource.src);
                ShowText(resource);
                break;
             case "url":
                Application.OpenURL(resource.src);
                break;
            default:
                Debug.Log("Abrindo aplicacao para o arquivo:" + resource.src);
                Application.OpenURL(Application.streamingAssetsPath + resource.src);
                break;
        }
    }

    private void ShowText(ClassResource resource)
    {
        var caminho = Application.streamingAssetsPath + resource.src;
        if (!File.Exists(caminho))
            return;
        var file = new FileStream(caminho, FileMode.Open, FileAccess.Read);
        var sr = new StreamReader(file);
        var conteudo = sr.ReadToEnd();
        textoPages.GetComponentInChildren<TextMeshProUGUI>().SetText(conteudo);
    }

    public IEnumerator LoadTextureInto(string path, Image img)
    {
        var www = UnityWebRequestTexture.GetTexture(path);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Get downloaded asset bundle
            var texture = DownloadHandlerTexture.GetContent(www);
            img.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }

        yield return 0;
    }
}