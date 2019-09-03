using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ClassAluno
{
    private const string CharacterPortraitLocation = "Illustrations/CharacterPortraits/Students/";
    public string deficiencia;
    public string descricao;
    public int id;
    public bool importante;
    public string nome;

    public Sprite LoadPortrait()
    {
        return Resources.Load<Sprite>(
            CharacterPortraitLocation + id);
    }
}
