using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StudentIcon : MonoBehaviour
{
    private ClassAluno _student;
    public Image image;

    public ClassAluno Student
    {
        get => _student;
        set
        {
            _student = value;
            Load();
        }
    }

    private void Load()
    {
        image.sprite = _student.LoadPortrait();
    }
}