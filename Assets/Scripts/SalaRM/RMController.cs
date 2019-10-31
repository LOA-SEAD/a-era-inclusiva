using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RMController : MonoBehaviour
{
    public GameObject buttonPrefab;
    public SimpleScroll scroll;
    public GameObject TextCanvas;
    public TextMeshProUGUI Text;

    private void Start()
    {
        AudioManager.instance.PlayMusic((int)SoundType.MusicRoom);
    }

    public void OnSelectType(string type)
    {
        if (!scroll.gameObject.active)
            scroll.gameObject.SetActive(true);

        scroll.Clear();
        List<GameObject> buttons = new List<GameObject>();
        foreach (var recurso in GameManager.GameData.RecursosRM.Where(x => x.Tipo == type))
        {
            var button = Instantiate(buttonPrefab).GetComponent<Button>();
            button.onClick.AddListener(() => OnSelectResource(recurso.Nome));
            button.GetComponentInChildren<TextMeshProUGUI>().text = recurso.Nome;
            buttons.Add(button.gameObject);
        }

        scroll.AddList(buttons);
    }

    private void OnSelectResource(string recursoNome)
    {
        TextCanvas.SetActive(true);
        Text.SetText(GameManager.GameData.RecursosRM.Find(x=>x.Nome == recursoNome).Descricao);
    }
}