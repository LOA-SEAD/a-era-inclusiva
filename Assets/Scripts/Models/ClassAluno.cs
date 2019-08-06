using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ClassAluno{
    public int id;
	public string nome;
    public string deficiencia;
    public string descricao;
    public bool importante;
    private const string CharacterPortraitLocation = "Illustrations/CharacterPortraits/Students/";

    public Sprite LoadPortrait()
    {
	    return Resources.Load<Sprite>(
		    CharacterPortraitLocation + id);
    }
}


public class ClassAlunos
{
	public List<ClassAluno> alunos;

	public ClassAlunos()
	{
		alunos = new List<ClassAluno>();
	}
}