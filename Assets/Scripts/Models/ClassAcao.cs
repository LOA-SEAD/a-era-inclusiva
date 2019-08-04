using System.Collections.Generic;

[System.Serializable]
public class ClassAcao
{
    public int id;
	public string nome;
    public string tipo;
    public bool selected;
    public string icone;

    public void ToggleSelection()
    {
        selected = !selected;
    }
}

[System.Serializable]
public class ClassAcoes
{
    public List<ClassAcao> acoes;
    public ClassAcoes()
    {
        acoes = new List<ClassAcao>();
    }

   
}
