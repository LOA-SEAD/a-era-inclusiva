using System.Collections.Generic;

[System.Serializable]
public class ClassDemanda
{


    public int idAluno;
    public int ordem;
    public string descricao;
    public int nivelUrgencia;
    public bool resolvida;
    public List<Efetividade> acoesEficazes;

    public ClassAluno student
    {
        get
        {
            return Game.Students.alunos.Find(x=>x.id == idAluno);
        }
    }
}
[System.Serializable]
public class Efetividade
{
    public int idAcao;
    public int efetividade;
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

