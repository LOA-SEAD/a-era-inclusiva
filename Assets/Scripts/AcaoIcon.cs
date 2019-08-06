using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AcaoIcon : MonoBehaviour
{

    private ClassAcao _acao;
    public TextMeshProUGUI iconObj;
    public TextMeshProUGUI nameObj;
    private Button _button;
   
    private void Awake()
    {
        _button = GetComponent<Button>();
    }
    
    public void AddListener(UnityAction action)
    {
        _button.onClick.AddListener(action);
    }

    public ClassAcao Acao
    {
        get { return _acao; }
        set
        {
            _acao = value;
            iconObj.SetText(_acao.icone);
            nameObj.SetText(_acao.nome);
            
        }
    }

    public void Toggle()
    {
        _acao.ToggleSelection();
    }

   

}
