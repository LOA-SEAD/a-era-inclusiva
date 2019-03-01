using System.Collections.Generic;

[System.Serializable]
public class ClassAluno{
    public int id;
	public string nome;
    public string deficiencia;
    public string descricao;
    public List<ClassDemanda> demandas;

}


public class ClassAlunos
{
	public List<ClassAluno> alunos;

	public ClassAlunos()
	{
		alunos = new List<ClassAluno>();
	}
}