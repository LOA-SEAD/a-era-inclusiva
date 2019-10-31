using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackContentHTPI : MonoBehaviour
{
    public HTPIController htpiController;
    public StudentIcon studentIconPrefab;
    public FeedbackController feedbackController;
    public GameObject solutionsParent;
    public SolutionPanel solutionPanelPrefab;
    public GameObject StudentIconParent;
    public void ShowResultsOf(ClassAluno student)
    {
        foreach (Transform children in solutionsParent.transform)
        {
            Destroy(children.gameObject);
        }   
        foreach (var solution in htpiController._resolucoes.Where(x=>x.Key.student == student))
        {
            var resultPanel = Instantiate(solutionPanelPrefab, solutionsParent.transform);
            resultPanel.SetStars(feedbackController.results[solution.Key]);
            resultPanel.SetText(solution.Key.descricao, solution.Value.nome);
        }
    }

    public void PopulateStudentList()
    {
        var important = GameManager.GameData.Alunos.Where(x => x.importante);
        foreach (var student in important)
        {
            var icon = Instantiate(studentIconPrefab,StudentIconParent.transform);
            icon.Student = student;
            icon.GetComponent<Button>().onClick.AddListener(()=>ShowResultsOf(student));
        }
    }

    public void Start()
    {
        PopulateStudentList();
    }
}
