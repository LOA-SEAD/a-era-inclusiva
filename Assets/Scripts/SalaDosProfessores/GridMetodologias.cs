using System.Collections.Generic;
using UnityEngine;

public class GridMetodologias : MonoBehaviour
{
    public AcaoIcon ActionPrefab;
    private List<ClassAcao> actionsToShow;
    public GameObject gridAcoes;

    public List<ClassAcao> ActionsToShow
    {
        get => actionsToShow;
        set
        {
            actionsToShow = value;
            CleanAndPopulate();
        }
    }

    // Start is called before the first frame update
    public void CleanAndPopulate()
    {
        foreach (Transform child in gridAcoes.transform) Destroy(child.gameObject);
        if (actionsToShow == null) return;

        foreach (var action in actionsToShow)
        {
            var actionObj = Instantiate(ActionPrefab, gridAcoes.transform);
            actionObj.Acao = action;
        }
    }
}