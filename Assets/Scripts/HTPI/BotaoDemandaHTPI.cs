using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BotaoDemandaHTPI : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TextMeshProUGUI tick;
    public TextMeshProUGUI exclamation;
    private ClassDemanda _demanda;

    public ClassDemanda Demanda
    {
        set
        {
            _demanda = value;
            text.SetText(_demanda.descricao);
            exclamation.SetText(new String('\uf12a', _demanda.nivelUrgencia));
        }
        get => _demanda;
    }
 
    public void Select()
    {
        tick.alpha = 1f;
    }
    // Update is called once per frame
}
