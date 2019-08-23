public class StudentList : SimpleScroll
{
    public delegate void StudentAction(ClassAluno aluno);

    public bool importantOnly;
    public StudentIcon prefabButton;

    private StudentAction whenSelected;

    public void SetWhenSelectedAction(StudentAction action)
    {
        whenSelected = action;
    }


    public void Start()
    {
        UpdateList();
    }

    private void UpdateList()
    {
        if (GameManager.GameData.Students == null || GameManager.GameData.Students.alunos == null) return;
        Clear();
        BackToTop();
        foreach (var student in GameManager.GameData.Students.alunos)
        {
            if (importantOnly && !student.importante) continue;

            var button = Instantiate(prefabButton);
            button.Student = student;
            if (whenSelected != null)
                button.AddListener(delegate { whenSelected(student); });
            Add(button.gameObject);
        }
    }
}