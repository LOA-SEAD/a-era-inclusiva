using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct AlunoComId
{
    public int id;
    public GameObject aluno;
}

public class AlunosSalaDeAula : MonoBehaviour
{
    public List<AlunoComId> alunos;
    public BalaoNotificacao balaoPrefab;

    public void MostrarBalao(ClassDemanda demanda)
    {
        var balao = Instantiate(balaoPrefab, alunos.Find(x => x.id == demanda.idAluno).aluno.transform, false);
        balao.Level = demanda.nivelUrgencia - 1;
    }
}