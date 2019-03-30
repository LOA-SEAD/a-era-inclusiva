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
    private int _level;
    private ClassAluno _student;
    private readonly List<Color32> colorForEachLevel = new List<Color32>
    {
        new Color32(75, 146, 103,255),
        new Color32(255, 149, 60,255),
        new Color32(226, 90, 72,255)
    };
    public Image background;
    public Image studentPhoto;
    public TextMeshProUGUI text;
    
    public  ClassDemanda Demand { get; set; }

    public int Level
    {
        private get { return _level; }
        set
        {
            _level = value;
            background.color = colorForEachLevel[_level-1];
            text.SetText(new string('!', _level));
        }
    }

    public ClassAluno Student
    {
        private get { return _student; }
        set
        {
            _student = value;
            studentPhoto.sprite = Resources.Load<Sprite>(
                _characterPortraitLocation + Student.id);
        }
    }

   

}