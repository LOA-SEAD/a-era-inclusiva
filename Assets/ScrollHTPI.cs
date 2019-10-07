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
    public Button DemandPrefab;
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
        StudentList.GoDown();   
    }

    public void GoUpStudent(object sender, EventArgs eventArgs)
    {
        
        StudentList.GoUp();
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
        PopulateDemandList(DemandByStudentList.First().Key);
    }

    public void PopulateStudentList()
    {       
        var list = new List<GameObject>();

        foreach (var student in DemandByStudentList.Keys)
        {
            var button = Instantiate(DemandPrefab);
            button.GetComponentInChildren<TextMeshProUGUI>().SetText(student.nome);
            list.Add(button.gameObject);
            button.onClick.AddListener(()=>PopulateDemandList(student));
        }
        StudentList.AddList(list);
    }

    public void PopulateDemandList(ClassAluno student)
    {
        DemandList.Clear();
        var list = new List<GameObject>();

        foreach (var demand in DemandByStudentList[student])
        {
            var button = Instantiate(DemandPrefab);
            button.GetComponentInChildren<TextMeshProUGUI>().SetText(demand.descricao);

            list.Add(button.gameObject);
            button.onClick.AddListener(()=>HtpiController.SelectDemand(demand));
        }

        DemandList.AddList(list);

    }
    public void GoToStudent(ClassAluno student)
    {
        PopulateDemandList(student);
    }

}
