using System.Linq;

public class ActionListSalaProfessores : ActionList
{
    public MetodologiasTab metodologiasTab;
    public PopupQuestion popupQuestion;
    protected override bool WhichActions(ClassAcao x)
    {
        return x.tipo == metodologiasTab.MetodologiaSelecionada;
    }

    protected override void OnSelect(ClassAcao acao)
    {
        acao.selected = !acao.selected;
        FindObjectOfType<GridMetodologias>().UpdateList();

        if (Game.Actions.acoes.Count(x => x.tipo == acao.tipo && x.selected) >= 3 && acao.selected)
        {
            popupQuestion.gameObject.SetActive(true);
        }
    }
}