using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedActionListHTPI : ActionList
{
    
    protected HTPIController htpiController;

    protected override bool WhichActions(ClassAcao x)
    {
        return htpiController.SelectedStudent!=null && htpiController.GetSelectedActions().Contains(x);
    }


    private new void Awake()
    {
    
        htpiController = FindObjectOfType<HTPIController>();
        if (htpiController == null)
        {
            Debug.Log("Não foi possivel encontrar o HTPIController");
        }

    }

    protected override void OnSelect(ClassAcao acao)
    {
        if (htpiController != null)
            htpiController.AddAction(acao);
    }

}
