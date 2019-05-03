using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ActionListHTPI : ActionList
{
    protected HTPIController htpiController;
    private string tipo = "Diálogos";

    public string Tipo
    {
        get => tipo;
        set
        {
            tipo = value;
            UpdateList();
        }
    }

    private new void Awake()
    {
    
        htpiController = FindObjectOfType<HTPIController>();
        if (htpiController == null)
        {
            Debug.Log("Não foi possivel encontrar o HTPIController");
        }
     

    }

    protected override bool WhichActions(ClassAcao x)
    {
        if(!string.IsNullOrEmpty(Tipo))
            return x.tipo==Tipo;
        return true;
    }

    protected override void OnSelect(ClassAcao acao)
    {
        if (htpiController != null)
            htpiController.AddAction(acao);
    }
}