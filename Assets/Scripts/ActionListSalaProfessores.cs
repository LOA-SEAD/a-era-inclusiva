using System.Linq;

public class ActionListSalaProfessores : ActionList
{
    public MetodologiasTab metodologiasTab;

    protected override bool WhichActions(ClassAcao x)
    {
        return x.tipo == metodologiasTab.MetodologiaSelecionada;
    }

    protected virtual void OnSelectAction(ClassAcao acao)
    {
        acao.selected = !acao.selected;
        FindObjectOfType<GridMetodologias>().UpdateList();

        if (Game.Actions.acoes.Count(x => x.tipo == acao.tipo && x.selected) >= 3 && acao.selected)
        {
            FindObjectOfType<MetodologiasTab>().ShowDialog();
        }
    }
}