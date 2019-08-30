using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MetodologiasTab : MonoBehaviour
{
    public ActionList actionListSalaProfessores;
    public ActionConfirmation confirmation;
    public GridMetodologias gridMetodologias;
    private int idMetodologia;

    private List<string> Metodologias;
    public TextMeshProUGUI Texto;
    public TextMeshProUGUI Titulo;

    public string MetodologiaSelecionada => Metodologias[idMetodologia];

    public int IdMetodologia
    {
        get => idMetodologia;
        set
        {
            idMetodologia = value;
            actionListSalaProfessores.UpdateList();
            Titulo.SetText(MetodologiaSelecionada);
            Texto.SetText(string.Format(
                "Escolha 3 ações da categoria {0} que você considera mais eficazes para esta aula, levando em consideração os perfis dos estudantes",
                MetodologiaSelecionada));
            SelectionHasChanged(value > idMetodologia);
        }
    }

    public bool HasNextMethodology()
    {
        return idMetodologia + 1 < Metodologias.Count;
    }

    private void Awake()
    {
        if (GameManager.GameData.Acoes != null)
            Metodologias = GameManager.GameData.Acoes.Select(x => x.tipo).Distinct().ToList();
        else
            Metodologias = new List<string>();
        IdMetodologia = 0;
        SelectionHasChanged(false);
        actionListSalaProfessores.SetWhenSelected(OnSelect);
    }


    public void GoToNextMethodology()
    {
        IdMetodologia++;
        actionListSalaProfessores.Type = MetodologiaSelecionada;
    }

    public void Reset()
    {
        IdMetodologia = 0;
        GameManager.GameData.Acoes.ForEach(x => x.selected = false);
    }

    public void Undo()
    {
        var actions = GameManager.GameData.Acoes.FindAll(x => x.tipo == MetodologiaSelecionada && x.selected);
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
        var selected = GameManager.GameData.Acoes.FindAll(x => x.tipo == MetodologiaSelecionada && x.selected);
        gridMetodologias.ActionsToShow = selected;
        if (notificate && selected.Count >= 3) Confirmation(selected);
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
            confirmation.SetText(string.Format(
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
        var actions = GameManager.GameData.Acoes.FindAll(x => x.tipo == MetodologiaSelecionada && x.selected);
        if (actions.Count >= 3 && !acao.selected) return false;

        acao.selected = !acao.selected;
        return true;
    }


    protected void OnSelect(ClassAcao acao)
    {
        if (TrySelect(acao))
            SelectionHasChanged(true);
    }
}