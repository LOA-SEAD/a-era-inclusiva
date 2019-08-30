using System;
using System.Collections.Generic;

[Serializable]
public class ClassDemanda
{
    public List<Efetividade> acoesEficazes;
    public string descricao;


    public int idAluno;
    public int nivelUrgencia;
    public int ordem;
    public bool resolvida;
    public bool selecionada;

    public ClassAluno student
    {
        get { return GameManager.GameData.alunos.Find(x => x.id == idAluno); }
    }
}

[Serializable]
public class Efetividade
{
    public int efetividade;
    public int idAcao;
}
