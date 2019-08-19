using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StudentIcon : Toggle
{
    private ClassAluno _student;
    public Image portraitObj;
    public TextMeshProUGUI nameObj;


    public ClassAluno Student
    {
        get { return _student; }
        set
        {
            _student = value;
            portraitObj.sprite = _student.LoadPortrait();
            ;
            if(nameObj!=null)
                nameObj.SetText(_student.nome);
        }
    }

  
    private Button _button;
   
    private new void Awake()
    {
        base.Awake();
        _button = GetComponent<Button>();
    }
    
    
    public void AddListener(UnityAction action)
    {
        _button.onClick.AddListener(action);
    }

}