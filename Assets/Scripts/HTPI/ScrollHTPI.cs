using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScrollHTPI : MonoBehaviour
{
    public HTPIController HtpiController;

    private Dictionary<ClassAluno, List<ClassDemanda>> DemandByStudentList;

    public SimpleScrollAlt DemandList;
    public SimpleScrollAlt StudentList;

    public BotaoDemandaHTPI DemandPrefab;
    public StudentIcon StudentPrefab;

    private ClassAluno _studentSelected;

    private void Awake()
    {
        DemandByStudentList = new Dictionary<ClassAluno, List<ClassDemanda>>();
    }

    void Start()
    {
        DemandList.BottomReached += GoDownStudent;
        DemandList.TopReached += GoUpStudent;

        if (GameManager.GameData != null &&
            GameManager.GameData.Demandas != null &&
            GameManager.GameData.Demandas.Count > 0)
        {
            Setup(this, EventArgs.Empty);
        }
        else
        {
            GameData.GameDataLoaded += Setup;
        }
    }

    public void GoDownStudent(object sender, EventArgs eventArgs)
    {
        if (StudentList.GoDown())
        {
            DemandList.SelectFirst();
        }
    }

    public void GoUpStudent(object sender, EventArgs eventArgs)
    {
        if (StudentList.GoUp())
        {
            DemandList.SelectLast();
        }
    }

    private void Setup(object obj, EventArgs empty)
    {
        DemandByStudentList.Clear();

        foreach (var student in GameManager.GameData.Alunos.Where(x => x.dia == GameManager.PlayerData.Day))
        {
            List<ClassDemanda> demandList = new List<ClassDemanda>();

            foreach (var demand in GameManager.GameData.Demandas.Where(x => x.idAluno == student.id && x.dia == GameManager.PlayerData.Day))
            {
                demandList.Add(demand);
            }

            DemandByStudentList[student] = demandList;
        }

        PopulateStudentList();

        StudentList.SelectFirst();

        if (DemandByStudentList.Count > 0)
        {
            _studentSelected = DemandByStudentList.First().Key;
            PopulateDemandList(_studentSelected);
            DemandList.SelectFirst();
        }
    }

    public void PopulateStudentList()
    {
        StudentList.Clear();

        var list = new List<Selectable>();

        foreach (var student in DemandByStudentList.Keys)
        {
            var studentIcon = Instantiate(StudentPrefab);

            studentIcon.Student = student;

            Button button = studentIcon.GetComponent<Button>();

            list.Add(button);

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() =>
            {
                _studentSelected = student;
                PopulateDemandList(student);
            });
        }

        StudentList.AddList(list);
    }

    public void PopulateDemandList(ClassAluno student)
    {
        DemandList.Clear();

        var list = new List<Selectable>();

        foreach (var demand in DemandByStudentList[student])
        {
            var button = Instantiate(DemandPrefab);

            button.Demanda = demand;

            Button unityButton = button.GetComponent<Button>();

            int pairNumber = HtpiController.GetPairNumber(demand);

            if (HtpiController.DemandIsResolved(demand))
            {
                button.Select(pairNumber);

                // Só a demanda fica bloqueada depois de escolher uma ação.
                unityButton.interactable = false;
            }
            else
            {
                button.ClearPair();
                unityButton.interactable = true;
            }

            list.Add(unityButton);

            unityButton.onClick.RemoveAllListeners();
            unityButton.onClick.AddListener(() =>
            {
                HtpiController.SelectDemand(button);
            });
        }

        DemandList.AddList(list);
    }

    public void RefreshDemandList()
    {
        if (_studentSelected != null)
        {
            PopulateDemandList(_studentSelected);
        }
    }
}