using System.Collections.Generic;

[System.Serializable]
public class ClassDemanda
{
    public int idAluno;
    public string descricao;
    public int nivelUrgencia;
    public bool resolvida;
    public Dictionary<ClassAcao,int> acoes;

    public ClassAluno student
    {
        get
        {
            return Game.Students.alunos.Find(x=>x.id == idAluno);
        }
    }
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

