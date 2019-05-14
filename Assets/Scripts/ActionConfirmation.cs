using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionConfirmation : Confirmation
{
    
    public GameObject actionsPanel;
    public AcaoIcon actionPrefab;
    private List<ClassAcao> actionsToShow;

    public List<ClassAcao> ActionsToShow
    {
        get => actionsToShow;
        set
        {
            actionsToShow = value;
            CleanAndPopulate();
        }
    }

    public void OnEnable()
    {
        CleanAndPopulate();
        
    }

    private void CleanAndPopulate()
    {
        foreach (Transform children in actionsPanel.transform)
        {
            Destroy(children.gameObject);
        }
        if (actionsToShow == null)
            return;
       

        foreach (var action in actionsToShow)
        {
            var actionButton = Instantiate(actionPrefab, actionsPanel.transform);
            actionButton.Acao = action;
        }
    }

}