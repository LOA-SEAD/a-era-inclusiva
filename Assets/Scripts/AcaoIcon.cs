using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AcaoIcon : Toggle
{

    private ClassAcao _acao;
    public TextMeshProUGUI iconObj;
   

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

   

}
