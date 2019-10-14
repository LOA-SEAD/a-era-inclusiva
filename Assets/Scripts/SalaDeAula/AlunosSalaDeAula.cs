using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct AlunoComId
{
    public int id;
    public GameObject aluno;
}

public class AlunosSalaDeAula : MonoBehaviour
{
    public Image[] alunos;
    public BalaoNotificacao balaoPrefab;

    public void MostrarBalao(ClassDemanda demanda)
    {
        var balao = Instantiate(balaoPrefab, alunos[demanda.idAluno].transform, false);
        balao.Level = demanda.nivelUrgencia - 1;
    }

    // Start is called before the first frame update
    private void Start()
    {
        if (GameManager.GameData != null && GameManager.GameData.Demandas != null &&
            GameManager.GameData.Demandas.Count > 0)
            Setup(this, EventArgs.Empty);
        else
        {
            GameData.GameDataLoaded += Setup;
        }
    }

    private void Setup(object sender, EventArgs e)
    {
        foreach (var student in GameManager.GameData.Alunos)
        {
            if (alunos.Length > student.id)
            {
                alunos[student.id].sprite = student.LoadImage();
                alunos[student.id].gameObject.SetActive(true);
                

            }
        }
        
    }

    // Update is called once per frame
    private void Update()
    {
    }
}