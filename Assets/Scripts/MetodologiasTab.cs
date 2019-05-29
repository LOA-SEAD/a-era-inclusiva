using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MetodologiasTab : MonoBehaviour
{
    public GridMetodologias gridMetodologias;
    private int idMetodologia = 0;

    private List<string> Metodologias;
    public TextMeshProUGUI Titulo;
    public TextMeshProUGUI Texto;
    public ActionListSalaProfessores actionListSalaProfessores;
    public ActionConfirmation confirmation;

    public string MetodologiaSelecionada
    {
        get { return Metodologias[idMetodologia]; }
    }

    public bool HasNextMethodology()
    {
        return idMetodologia + 1 < Metodologias.Count;
    }

    public int IdMetodologia
    {
        get { return idMetodologia; }
        set
        {
            idMetodologia = value;
            actionListSalaProfessores.UpdateList();
            Titulo.SetText(MetodologiaSelecionada);
            Texto.SetText(String.Format(
                "Escolha 3 ações da categoria {0} que você considera mais eficazes para esta aula, levando em consideração os perfis dos estudantes",
                MetodologiaSelecionada));
            SelectionHasChanged(value > idMetodologia);
        }
    }

    private void Awake()
    {
        if (Game.Actions != null)
            Metodologias = Game.Actions.acoes.Select(x => x.tipo).Distinct().ToList();
        else
            Metodologias = new List<string>();
        IdMetodologia = 0;
        SelectionHasChanged(false);
    }


    public void GoToNextMethodology()
    {
        IdMetodologia++;
    }

    public void Reset()
    {
        IdMetodologia = 0;
        Game.Actions.acoes.ForEach(x => x.selected = false);
    }

    public void Undo()
    {
        var actions = Game.Actions.acoes.FindAll(x => x.tipo == MetodologiaSelecionada && x.selected);
        if (actions.Count == 0)
        {
            IdMetodologia = idMetodologia > 0 ? idMetodologia - 1 : 0;
        }
        else
        {
            actions.ForEach(x => x.selected = false);
            gridMetodologias.ActionsToShow = null;
        }

        
    }



    public void SelectionHasChanged(bool notificate)
    {
        var selected = Game.Actions.acoes.FindAll(x => x.tipo == MetodologiaSelecionada && x.selected);
        gridMetodologias.ActionsToShow = selected;
        if (notificate && selected.Count >= 3)
        {
            Confirmation(selected);
        }
    }

    private void Confirmation(List<ClassAcao> selected)
    {
        confirmation.gameObject.SetActive(true);
        confirmation.ActionsToShow = selected;
        if (HasNextMethodology())
        {
            confirmation.OnAccept(delegate
            {
                GoToNextMethodology();
                confirmation.Hide();
            });
            confirmation.SetText(String.Format(
                "Você selecionou as seguintes ações na categoria {0}, você deseja avançar para a proxima ou alterar suas escolhas?",
                MetodologiaSelecionada));
        }
        else
        {
            confirmation.OnAccept(delegate { Initiate.Fade("Scenes/SalaDeAula", Color.black, 3); });
            confirmation.SetText("Você terminou a seleção, deseja ir para a aula?");
        }

        confirmation.OnDeny(confirmation.Hide);
    }

    public bool TrySelect(ClassAcao acao)
    {
        var actions = Game.Actions.acoes.FindAll(x => x.tipo == MetodologiaSelecionada && x.selected);
        if (actions.Count >= 3 && !acao.selected)
        {
            return false;
        }

        acao.selected = !acao.selected;
        return true;
    }
}