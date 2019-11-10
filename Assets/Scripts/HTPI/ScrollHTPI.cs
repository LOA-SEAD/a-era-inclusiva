using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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
    // Start is called before the first frame update
    private void Awake()
    {
        DemandByStudentList = new Dictionary<ClassAluno, List<ClassDemanda>>();
    }

    void Start()
    {
        DemandList.BottomReached += GoDownStudent;
        DemandList.TopReached += GoUpStudent;

        if (GameManager.GameData != null && GameManager.GameData.Demandas != null &&
            GameManager.GameData.Demandas.Count > 0)
            Setup(this, EventArgs.Empty);
        else
        {
            GameData.GameDataLoaded += Setup;
        }
    }

    public void GoDownStudent(object sender, EventArgs eventArgs)
    {
        if(StudentList.GoDown())   
            DemandList.SelectFirst();
    }

    public void GoUpStudent(object sender, EventArgs eventArgs)
    {
        if(StudentList.GoUp())
            DemandList.SelectLast();
    }
    

    private void Setup(object obj, EventArgs empty)
    {
        foreach (var student in GameManager.GameData.Alunos.Where(x=>x.importante))
        {
            List<ClassDemanda> demandList = new List<ClassDemanda>();
            foreach (var demand in GameManager.GameData.Demandas.Where(x=>x.idAluno == student.id))
            {
                demandList.Add(demand);
            }

            DemandByStudentList[student] = demandList;
        }
        PopulateStudentList();
        StudentList.SelectFirst();
        PopulateDemandList(DemandByStudentList.First().Key);
        DemandList.SelectFirst();
    }

    public void PopulateStudentList()
    {    
        StudentList.Clear();
        var list = new List<Selectable>();

        foreach (var student in DemandByStudentList.Keys)
        {
            var studentIcon = Instantiate(StudentPrefab);
            studentIcon.Student = student;
            list.Add(studentIcon.GetComponent<Button>());
            studentIcon.GetComponent<Button>().onClick.AddListener(()=>PopulateDemandList(student));
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
            if (HtpiController._resolucoes[demand] != null)
            {
                button.GetComponent<Button>().interactable = false;
            }
            list.Add(button.GetComponent<Button>());
            button.GetComponent<Button>().onClick.AddListener(()=>HtpiController.SelectDemand(button));
        }
        DemandList.AddList(list);

    }

}
