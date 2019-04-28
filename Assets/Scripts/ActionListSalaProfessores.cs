using System.Linq;

public class ActionListSalaProfessores : ActionList
{
    public MetodologiasTab metodologiasTab;
    public ActionConfirmation popupQuestion;
    protected override bool WhichActions(ClassAcao x)
    {
        return x.tipo == metodologiasTab.MetodologiaSelecionada;
    }

    protected override void OnSelect(ClassAcao acao)
    {
        acao.selected = !acao.selected;
        metodologiasTab.SelectionHasChanged(true);
  
    }
}