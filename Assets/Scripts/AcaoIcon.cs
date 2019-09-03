using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AcaoIcon : MonoBehaviour
{
    private ClassAcao _acao;
    private Button _button;
    public TextMeshProUGUI iconObj;
    public TextMeshProUGUI nameObj;

    public ClassAcao Acao
    {
        get => _acao;
        set
        {
            _acao = value;
            iconObj.SetText(_acao.icone);
            nameObj.SetText(_acao.nome);
        }
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void AddListener(UnityAction action)
    {
        _button.onClick.AddListener(action);
    }

    public void Toggle()
    {
        _acao.ToggleSelection();
    }
}