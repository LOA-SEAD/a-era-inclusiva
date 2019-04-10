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
    private int idMetodologia = 0;
    
    private List<string> Metodologias;
    public TextMeshProUGUI Titulo;
    public TextMeshProUGUI Texto;
    public Button UndoButton;
    public ActionListSalaProfessores actionListSalaProfessores;
    public GameObject ExitButton;

    public string MetodologiaSelecionada
    {
        get { return Metodologias[idMetodologia]; }
    }

    public bool HasNextMethodology()
    {
        return idMetodologia >= Metodologias.Count - 1;
    }
    public int IdMetodologia
    {
        get { return idMetodologia; }
        set
        {
            idMetodologia = value;
            actionListSalaProfessores.UpdateList();
            Titulo.SetText(MetodologiaSelecionada);
            gridMetodologias.UpdateList();


        }
    }

 
    private void Awake()
    {
        
        if (Game.Actions != null)
            Metodologias = Game.Actions.acoes.Select(x => x.tipo).Distinct().ToList();
        else
            Metodologias = new List<string>();
    }



    public void GoToNextMethodology()
    {
        IdMetodologia++;
        Texto.SetText(String.Format("Escolha 3 {0} que você considera mais eficazes para esta aula, levando em consideração os perfis dos estudantes", MetodologiaSelecionada));
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
            IdMetodologia = idMetodologia > 0 ? idMetodologia-1 : 0;

        }
        else
        {
            actions.ForEach(x=>x.selected = false);
            gridMetodologias.UpdateList();
        }
        ExitButton.GetComponentInChildren<TextMeshProUGUI>().SetText("\uf00d");
        ExitButton.GetComponent<Image>().color = new Color32(255,80,66, 255);
        
    }

    public void Done()
    {
        ExitButton.GetComponentInChildren<TextMeshProUGUI>().SetText("\uf00c");
        ExitButton.GetComponent<Image>().color = Color.green;
       
    }
    
}