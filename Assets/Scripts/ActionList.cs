
using System.Linq;

public class ActionList : SimpleScroll
{
    public AcaoIcon actionPrefab;

    public new void Start()
    {
        base.Start();

        UpdateList();

    }

    public void UpdateList()
    {
        if (Game.Actions == null) return;
        Clear();
        BackToTop();
        foreach (var action in Game.Actions.acoes.Where(WhichActions))
        {
            var acaoIcon = Instantiate(actionPrefab);
            acaoIcon.Acao = action;
            acaoIcon.AddListener(delegate { OnSelect(action); });
            Add(acaoIcon.gameObject);
        }


    }

    protected virtual void OnSelect(ClassAcao action)
    {
        throw new System.NotImplementedException();
    }


    protected virtual bool WhichActions(ClassAcao acao)
    {
        return true;
    }

  
}
