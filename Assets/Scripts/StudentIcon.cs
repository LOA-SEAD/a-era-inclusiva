using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

public class StudentIcon : Toggle
{
    private Button _button;
    private ClassAluno _student;
    public TextMeshProUGUI nameObj;
    public Image portraitObj;


    public ClassAluno Student
    {
        get => _student;
        set
        {
            _student = value;
            portraitObj.sprite = _student.LoadPortrait();
            ;
            if (nameObj != null)
                nameObj.SetText(_student.nome);
        }
    }

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