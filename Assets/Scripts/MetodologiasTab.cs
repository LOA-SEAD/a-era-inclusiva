using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MetodologiasTab : MonoBehaviour
{
    public GridMetodologias gridMetodologias;
    public ActionList actionList;
    private int idMetodologia = 0;
    private List<string> Metodologias;
    public TextMeshProUGUI Titulo;
    public TextMeshProUGUI Texto;
    public Button UndoButton;
    public PopupQuestion popupQuestion;

    public string MetodologiaSelecionada
    {
        get { return Metodologias[idMetodologia]; }
    }

    private void Start()
    {
        UndoButton.interactable = false;
        
    }
    private void Awake()
    {
        
        if (Game.Actions != null)
            Metodologias = Game.Actions.acoes.Select(x => x.tipo).Distinct().ToList();
        else
            Metodologias = new List<string>();
    }

    public void ShowDialog()
    {
        popupQuestion.DenyButton.onClick.RemoveAllListeners();
        popupQuestion.AcceptButton.onClick.RemoveAllListeners();

        string message = String.Empty;
        if (idMetodologia >= Metodologias.Count-1)
        {
            message = "Você terminou de selecionar todas as ações, deseja finalizar ou começar novamente?";
            popupQuestion.AcceptButton.onClick.AddListener(delegate { popupQuestion.gameObject.SetActive(false); });
            popupQuestion.DenyButton.GetComponentInChildren<TextMeshProUGUI>().SetText("\uf0e2");
            popupQuestion.DenyButton.onClick.AddListener(Reset);
        }
        else
        {
            
            message = String.Format(
                "Você já selecionou 3 ações da categoria {0}\nDeseja avançar para a próxima categoria?",
                MetodologiaSelecionada);
            popupQuestion.AcceptButton.onClick.AddListener(goToNextMetodologia);
            popupQuestion.DenyButton.onClick.AddListener(delegate { popupQuestion.gameObject.SetActive(false); });
        }

        popupQuestion.gameObject.SetActive(true);
        popupQuestion.Message.SetText(message);
    }

    private void goToNextMetodologia()
    {
        if (!UndoButton.interactable)
            UndoButton.interactable = true;
        idMetodologia++;
        Titulo.SetText(MetodologiaSelecionada);
        Texto.SetText(String.Format("Escolha 3 {0} que você considera mais eficazes para esta aula, levando em consideração os perfis dos estudantes", MetodologiaSelecionada));
        gridMetodologias.UpdateList();
        actionList.UpdateList();
        popupQuestion.gameObject.SetActive(false);
    }

    private void Reset()
    {
        idMetodologia = 0;
        Game.Actions.acoes.ForEach(x => x.selected = false);
        Titulo.SetText(MetodologiaSelecionada);
        gridMetodologias.UpdateList();
        actionList.UpdateList();
        popupQuestion.gameObject.SetActive(false);
    }

    public void Undo()
    {
        Game.Actions.acoes.Where(x=>x.tipo == MetodologiaSelecionada).ToList().ForEach(x => x.selected = false);
        idMetodologia = idMetodologia > 0 ? idMetodologia-1 : 0;
        Titulo.SetText(MetodologiaSelecionada);
        gridMetodologias.UpdateList();
        actionList.UpdateList();
        popupQuestion.gameObject.SetActive(false);
    }
    
}