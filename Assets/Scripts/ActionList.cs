using System.Linq;

public class ActionList : SimpleScroll
{
    public delegate void AcaoAction(ClassAcao acao);

    public delegate bool AcaoFilter(ClassAcao acao);

    private string _type = "";

    public AcaoFilter actionFilter;
    public AcaoIcon actionPrefab;
    public bool selectedOnly;

    public AcaoAction whenSelected;


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
        if (GameManager.GameData.Acoes == null ) return;
        Clear();
        BackToTop();
        foreach (var action in GameManager.GameData.Acoes.Where(WhichActions))
        {
            var acaoIcon = Instantiate(actionPrefab);
            acaoIcon.Acao = action;
            if (whenSelected != null)
                acaoIcon.AddListener(delegate { whenSelected(action); });
            Add(acaoIcon.gameObject);
        }
    }

    public override void UpdateChildrenCount()
    {
        childrenCount = GameManager.GameData.Acoes.Where(WhichActions).Count();
    }

    public void SetWhenSelected(AcaoAction func)
    {
        whenSelected = func;
    }

    public void SetAcaoFilter(AcaoFilter func)
    {
        actionFilter = func;
    }

    protected virtual bool WhichActions(ClassAcao x)
    {
        if (actionFilter != null) return actionFilter(x);

        if (selectedOnly && !GameManager.PlayerData.SelectedActions.Contains(x)) return false;
        if (!string.IsNullOrEmpty(Type))
            return x.tipo == Type;
        return true;
    }
}