using System;
using System.Collections.Generic;
using System.Linq;

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
        get { return GameManager.GameData.Alunos.Find(x => x.id == idAluno); }
    }

    public int EfficiencyOf(ClassAcao action)
    {
        var efetividade = acoesEficazes.FirstOrDefault(x => x.idAcao == action.id);
        return efetividade?.efetividade ?? 0;
    }
}

[Serializable]
public class Efetividade
{
    public int efetividade;
    public int idAcao;
}