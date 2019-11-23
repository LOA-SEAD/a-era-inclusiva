using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackContentHTPI : MonoBehaviour
{
    public HTPIController htpiController;
    public StudentIcon studentIconPrefab;
    public FeedbackController feedbackController;
    public GameObject solutionsParent;
    public SolutionPanel solutionPanelPrefab;
    public SimpleScrollAlt studentList;
    public TextMeshProUGUI points;
    public static Dictionary<int, int> starByPoints = new Dictionary<int, int>() {{100, 3}, {50, 2}, {25, 1}, {0, 0}};

    private string pointsTemplate = @"<size=30>Pontos</size><br><b>{0}</b>";
    public void ShowResultsOf(ClassAluno student)
    {
        foreach (Transform children in solutionsParent.transform)
        {
            Destroy(children.gameObject);
        }   
        foreach (var solution in htpiController._resolucoes.Where(x=>x.Key.student == student))
        {
            GameManager.PlayerData.Points += feedbackController.results[solution.Key] / 5;
            var resultPanel = Instantiate(solutionPanelPrefab, solutionsParent.transform);
            resultPanel.SetStars(starByPoints[feedbackController.results[solution.Key]]);
            resultPanel.SetText(solution.Key.descricao, solution.Value.nome);
        }
    }

    public void PopulateStudentList()
    {
        var important = GameManager.GameData.Alunos.Where(x => x.importante);
        foreach (var student in important)
        {
            var icon = Instantiate(studentIconPrefab);
            icon.Student = student;
            icon.GetComponent<Button>().onClick.AddListener(()=>ShowResultsOf(student));
            studentList.Add(icon.GetComponent<Button>());
        }
        studentList.SelectFirst();
    }

    public void Start()
    {
        PopulateStudentList();
        studentList.BottomReached += feedbackController.ShowConfirmation;
        points.SetText(string.Format(pointsTemplate,GameManager.PlayerData.Points));
    }
}
