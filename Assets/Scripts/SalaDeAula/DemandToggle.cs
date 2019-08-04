using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DemandToggle : MonoBehaviour,ISelectHandler 
{
    private ClassDemanda _demand;
    private readonly List<Color32> colorForEachLevel = new List<Color32>
    {
        new Color32(75, 146, 103,255),
        new Color32(255, 149, 60,255),
        new Color32(226, 90, 72,255)
    };
    public Image background;
    public Image studentPhoto;
    public TextMeshProUGUI text;
    public Shadow shadow;


    public ClassDemanda Demand
    {
        get { return _demand; }
        set
        {
            _demand = value;
            var color = colorForEachLevel[_demand.nivelUrgencia-1];
            background.color = color;
            text.SetText(new string('\uf12a', _demand.nivelUrgencia));
            text.color = color;
            shadow.effectColor = color; 
            studentPhoto.sprite = _demand.student.LoadPortrait();
        }
    }
    public void OnSelect(BaseEventData eventData)
    {
        // Do something.
        FindObjectOfType<ControladorSalaDeAula>().SelectedDemand = this;
    }
   
    
   

}