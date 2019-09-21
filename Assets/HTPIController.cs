using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HTPIController : MonoBehaviour
{
    private bool _loaded; 
    public Button buttonPrefab;
    public SimpleScroll demandList;
    private ClassDemanda _demanda;

    public SimpleScroll actionList;

    // Start is called before the first frame update
    void Awake()
    {
        if(GameManager.GameData != null && GameManager.GameData.Demandas!=null && GameManager.GameData.Demandas.Count > 0)
            Setup(null, EventArgs.Empty);
         else {
            GameData.GameDataLoaded += Setup;
        }
    }

    void Setup(object sender, EventArgs e) {
        var buttonList = new List<GameObject>();
        foreach(ClassDemanda demanda in GameManager.GameData.Demandas) {
            var button  = Instantiate(buttonPrefab);
            button.GetComponentInChildren<TextMeshProUGUI>().text=demanda.descricao;
            button.onClick.AddListener(()=>DemandSelected(demanda));
            buttonList.Add(button.gameObject);
            
        }
        demandList.AddList(buttonList);
    }
    void DemandSelected(ClassDemanda demanda) {
        _demanda = demanda;
        

    }
    // Update is called once per frame
  
}
