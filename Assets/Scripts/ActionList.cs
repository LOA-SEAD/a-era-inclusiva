using System;
using System.Linq;

public class ActionList : SimpleScroll
{
    public AcaoIcon actionPrefab;
    private string _type = "";
    public bool selectedOnly = false;
    

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
        if (GameManager.GameData.Actions == null) return;
        Clear();
        BackToTop();
        foreach (var action in GameManager.GameData.Actions.acoes.Where(WhichActions))
        {
            var acaoIcon = Instantiate(actionPrefab);
            acaoIcon.Acao = action;
            if(whenSelected!=null)
                acaoIcon.AddListener(delegate { whenSelected(action); });
            Add(acaoIcon.gameObject);
        }

    }
    public override void UpdateChildrenCount()
    {
        childrenCount = GameManager.GameData.Actions.acoes.Where(WhichActions).Count();
    }

    public delegate void AcaoAction(ClassAcao acao);

    public delegate bool AcaoFilter(ClassAcao acao);

    public AcaoFilter actionFilter;
    
    public AcaoAction whenSelected;

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
        if (actionFilter!=null)
        {
            return actionFilter(x);
        }

        if (selectedOnly && !x.selected)
        {
            return false;
        }
        if (!string.IsNullOrEmpty(Type))
            return x.tipo == Type;
        return true;
    }
}