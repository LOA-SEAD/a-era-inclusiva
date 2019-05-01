
using System.Linq;

public class ActionList : ScrollList<ClassAcao>
{
    public AcaoIcon actionPrefab;

    public new void Start()
    {
        base.Start();

        UpdateList();

    }

    public void UpdateList()
    {
        Clear();
        if (Game.Actions == null) return;
        
        
        
        foreach (var action in Game.Actions.acoes.Where(WhichActions))
        {
            var acaoIcon = Instantiate(actionPrefab);
            acaoIcon.Acao = action;
            acaoIcon.AddListener(delegate { OnSelect(action); });
            AddGameObject(acaoIcon.transform);
        }

        Selected=0;

    }

    protected virtual bool WhichActions(ClassAcao acao)
    {
        return true;
    }

  
}
