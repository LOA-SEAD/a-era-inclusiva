using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game
{
    // Start is called before the first frame update
    public static List<ClassAlunos> Students;
    public static List<ClassMetodologias> Methodologies;
    public static List<ClassMetodologias> MethodologiesSelected;
    public static List<ClassFalas> Dialogs;

    public static int Happiness; 

    public static void Setup()
    {

        Happiness = 0;
        Students = new List<ClassAlunos>();
        Methodologies = new List<ClassMetodologias>();
        MethodologiesSelected = new List<ClassMetodologias>();
        Dialogs = new List<ClassFalas>();
    }
}
