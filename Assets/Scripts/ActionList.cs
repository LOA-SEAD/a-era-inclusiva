
using System.Linq;

public class ActionList : ScrollList
{
    public AcaoIcon actionPrefab;

    public new void Start()
    {
        base.Start();

        if (Game.Actions == null) return;
        
        foreach (var action in Game.Actions.acoes.Where(WhichActions))
        {
            var acaoIcon = Instantiate(actionPrefab);
            acaoIcon.Acao = action;
            acaoIcon.AddListener(delegate { OnSelectAction(action); });
            AddGameObject(acaoIcon.transform);
        }

        UpdateShown();

    }

    protected virtual bool WhichActions(ClassAcao acao)
    {
        return true;
    }

    protected virtual void OnSelectAction(ClassAcao acao)
    {
        
    }
}
