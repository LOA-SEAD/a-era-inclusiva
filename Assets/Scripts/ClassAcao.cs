using System.Collections.Generic;

[System.Serializable]
public class ClassAcao{
	public string nome;
    public string tipo;
    public bool selected;
    public string icone;

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
