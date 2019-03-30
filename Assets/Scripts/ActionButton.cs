using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActionButton : MonoBehaviour
{
    public TextMeshProUGUI icon;
    public TextMeshProUGUI name;
    private ClassAcao _action;
    public ClassAcao Action
    {
        get { return _action; }
        set
        {
            _action = value;
            icon.SetText(_action.icone);


            name.SetText(_action.nome);
        }
    }

}
