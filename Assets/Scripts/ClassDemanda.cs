using System.Collections.Generic;

[System.Serializable]
public class ClassDemanda
{
    public string aluno;
    public string descricao;
    public string frase;

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

