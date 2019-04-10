using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActionButton : MonoBehaviour
{
    public TextMeshProUGUI iconObj;
    public TextMeshProUGUI nameObj;
    private ClassAcao _action;
    public ClassAcao Action
    {
        get { return _action; }
        set
        {
            _action = value;
            iconObj.SetText(_action.icone);


            nameObj.SetText(_action.nome);
        }
    }

    public void OnSelect()
    {
        FindObjectOfType<ControladorSalaDeAula>().UseAction(_action);
    }

}
