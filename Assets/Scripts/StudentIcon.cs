using UnityEngine;
using UnityEngine.UI;

public class StudentIcon : Toggle
{
    private ClassAluno _student;
    public Image portraitObj;
    

    public ClassAluno Student
    {
        get { return _student; }
        set
        {
            _student = value;
            portraitObj.sprite = _student.LoadPortrait();
            ;
            name = _student.nome;
        }
    }
}