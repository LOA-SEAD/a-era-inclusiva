using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AcaoIcon : MonoBehaviour
{
    public TextMeshProUGUI icon;
    public TextMeshProUGUI name;
    
    private ClassAcao _acao;
    

    public ClassAcao Acao
    {
        get { return _acao; }
        set
        {
            _acao = value;
            icon.SetText(_acao.icone);
            name.SetText(_acao.nome);
            
        }
    }

    public void Toggle()
    {
        _acao.selected = !_acao.selected;
        FindObjectOfType<GridMetodologias>().UpdateList();

        if (Game.Actions.acoes.Count(x => x.tipo == _acao.tipo && x.selected) >= 3 && _acao.selected)
        {
            FindObjectOfType<MetodologiasTab>().ShowDialog();
                
        }

    }

}
