using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    public AcaoIcon prefabBotaoAcao;

    void Awake()
    {
        foreach (var action in Game.Actions.acoes.FindAll(x => x.selected))
        {
            var actionButton = Instantiate(prefabBotaoAcao, transform);
            actionButton.Acao = action;
        }
    }
}