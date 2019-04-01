using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActionList : MonoBehaviour
{
    public AcaoIcon actionPrefab;
    private ScrollList scrollList;
    public MetodologiasTab metodologiasTab;
    private void Start()
    {
        scrollList = GetComponent<ScrollList>();
        UpdateList();
        
      
    }

    public void UpdateList()
    {
        scrollList.Clear();

        if (Game.Actions == null) return;
        
        foreach (var action in Game.Actions.acoes.Where(x=>x.tipo == metodologiasTab.MetodologiaSelecionada))
        {
            var acaoIcon = Instantiate(actionPrefab);
            acaoIcon.Acao = action;
            scrollList.AddGameObject(acaoIcon.transform);
        }

    }
}
