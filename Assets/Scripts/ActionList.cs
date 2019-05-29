using System.Linq;

public class ActionList : SimpleScroll
{
    public AcaoIcon actionPrefab;
    private string _type;

    public string Type
    {
        get => _type;
        set
        {
            _type = value;
            UpdateList();
        }
    }

    public void Start()
    {
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
    public override void UpdateChildrenCount()
    {
        childrenCount = Game.Actions.acoes.Where(WhichActions).Count();
    }

    protected virtual void OnSelect(ClassAcao action)
    {
        throw new System.NotImplementedException();
    }


    protected virtual bool WhichActions(ClassAcao x)
    {
        if (!string.IsNullOrEmpty(Type))
            return x.tipo == Type;
        return true;
    }
}