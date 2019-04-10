using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GridMetodologias : MonoBehaviour
{
    public GameObject gridAcoes;
    public AcaoIcon ActionPrefab;
    public MetodologiasTab metodologiasTab;

    // Start is called before the first frame update
    public void UpdateList()
    {
        foreach (Transform child in gridAcoes.transform)
        {
            Destroy(child.gameObject);
        }

        var actions = Game.Actions.acoes.FindAll(x => x.tipo == metodologiasTab.MetodologiaSelecionada && x.selected);

        foreach (var action in actions)
        {
            var actionObj = Instantiate(ActionPrefab, gridAcoes.transform);
            actionObj.Acao = action;
        }

    }
}