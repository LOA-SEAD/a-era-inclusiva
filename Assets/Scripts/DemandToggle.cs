using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DemandToggle : MonoBehaviour
{
    private readonly string _characterPortraitLocation = "Illustrations/CharacterPortraits/Students/";
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

    public ClassDemanda Demand
    {
        get { return _demand; }
        set
        {
            _demand = value;
            background.color = colorForEachLevel[_demand.nivelUrgencia-1];
            text.SetText(new string('!', _demand.nivelUrgencia));
            studentPhoto.sprite = Resources.Load<Sprite>(
                _characterPortraitLocation + _demand.student.id);
        }
    }

    public void OnSelect()
    {
        FindObjectOfType<ControladorSalaDeAula>().SelectedDemand = this;
    }

    public void Start()
    {
        
        if(_demand.nivelUrgencia >= Game.UrgenciaMinima)
        FindObjectOfType<ControladorSalaDeAula>().DemandCounter+= _demand.nivelUrgencia;

    }
    public void OnDestroy()
    {
        if(_demand.nivelUrgencia >= Game.UrgenciaMinima)
        FindObjectOfType<ControladorSalaDeAula>().DemandCounter-= _demand.nivelUrgencia;

    }

   

}