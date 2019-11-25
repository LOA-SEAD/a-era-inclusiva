using Coffee.UIExtensions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AvatarHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject personagem;
    public GameObject holofote;
    public GameObject sombra;
    public GameObject painel;

    private UIEffect uiEffect;
    private Image painelImagem;
    private TextMeshProUGUI painelTexto;

    private void Start()
    {
        uiEffect = personagem.GetComponent<UIEffect>();
        painelImagem = painel.GetComponent<Image>();
        painelTexto = painel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        uiEffect.colorFactor = 0f; // Clareia personagem
        sombra.SetActive(true);
        holofote.SetActive(true);
        painelImagem.color = new Color32(255, 109, 88, 255); // Painel laranja
        painelTexto.color = new Color32(255, 255, 255, 255); // Texto branco
        painelTexto.fontStyle = FontStyles.Bold;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        uiEffect.colorFactor = 0.4f; // Escurece personagem
        sombra.SetActive(false);
        holofote.SetActive(false);
        painelImagem.color = new Color32(255, 255, 255, 255); // Painel branco
        painelTexto.color = new Color32(47, 80, 89, 255);     // Texto azul escuro
        painelTexto.fontStyle = FontStyles.Normal;
    }
}
