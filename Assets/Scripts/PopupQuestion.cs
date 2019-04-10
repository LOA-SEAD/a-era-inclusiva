using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupQuestion : MonoBehaviour
{
    public Button DenyButton;

    public Button AcceptButton;

    public TextMeshProUGUI Message;

    public GameObject ActionsPanel;

    public AcaoIcon actionPrefab;
    public MetodologiasTab metodolodiasTab;

    public void OnEnable()
    {
        CleanAndPopulate();
        UpdateMessage();
    }

    public void CleanAndPopulate()
    {
        var actions = Game.Actions.acoes.FindAll(x => x.selected && x.tipo == metodolodiasTab.MetodologiaSelecionada);
        foreach (Transform children in ActionsPanel.transform)
        {
            Destroy(children.gameObject);
        }

        foreach (var action in actions)
        {
            var actionButton = Instantiate(actionPrefab, ActionsPanel.transform);
            actionButton.Acao = action;
        }
    }

    public void UpdateMessage()
    {
        DenyButton.onClick.RemoveAllListeners();
        AcceptButton.onClick.RemoveAllListeners();

        string message = String.Empty;
        if (metodolodiasTab.HasNextMethodology())
        {
            message = "Você terminou de selecionar todas as ações, deseja finalizar ou começar novamente?";
            AcceptButton.onClick.AddListener(metodolodiasTab.Done);

            AcceptButton.onClick.AddListener(delegate { gameObject.SetActive(false); });
            DenyButton.GetComponentInChildren<TextMeshProUGUI>().SetText("\uf0e2");
            DenyButton.onClick.AddListener(metodolodiasTab.Reset);
            DenyButton.onClick.AddListener(delegate { gameObject.SetActive(false); });

        }
        else
        {
            message = String.Format(
                "Você já selecionou 3 ações da categoria {0}\nDeseja avançar para a próxima categoria?",
                metodolodiasTab.MetodologiaSelecionada);
            AcceptButton.onClick.AddListener(metodolodiasTab.GoToNextMethodology);
            AcceptButton.onClick.AddListener(delegate { gameObject.SetActive(false); });
            DenyButton.onClick.AddListener(delegate { gameObject.SetActive(false); });
        }

        gameObject.SetActive(true);

        Message.SetText(message);
    }
}