using System.Collections.Generic;

[System.Serializable]
public class ClassDemanda
{
    public string aluno;
    public string descricao;
    public bool resolvida;
    public Dictionary<ClassAcao,int> acoes;

}
[System.Serializable]
public class ClassDemandas
{
    public List<ClassDemanda> demandas;
    public ClassDemandas()
    {
        demandas = new List<ClassDemanda>();
    }
}

