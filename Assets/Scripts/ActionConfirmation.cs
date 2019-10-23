using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionConfirmation : Confirmation
{
    public AcaoIcon actionPrefab;

    public GameObject actionsPanel;
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
        foreach (Transform children in actionsPanel.transform) Destroy(children.gameObject);
        if (actionsToShow == null)
            return;


        foreach (var action in actionsToShow)
        {
            var actionButton = Instantiate(actionPrefab, actionsPanel.transform);
            // TODO: Botões dessa confirmação não deveriam ter som, mas não podem aparecer desbotados
            //actionButton.GetComponent<Button>().interactable = false;
            actionButton.Acao = action;
        }
    }
}