
using UnityEngine;

    public class StudentListSalaProfessores:StudentList
    {
        public TabContentAlunos tca;
        protected override void OnSelect(ClassAluno aluno)
        {
               tca.SetAluno(aluno);
            
        }
    }
