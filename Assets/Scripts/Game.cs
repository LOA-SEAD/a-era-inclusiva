using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game
{
    // Start is called before the first frame update
    public static List<ClassAlunos> Alunos;
    public static List<ClassMetodologias> Metodologias;
    public static List<ClassMetodologias> MetodologiasSelecionadas;
    public static List<ClassFalas> Falas;

    public static int Happiness; 

    public static void Setup()
    {

        Happiness = 0;
        Alunos = new List<ClassAlunos>();
        Metodologias = new List<ClassMetodologias>();
        MetodologiasSelecionadas = new List<ClassMetodologias>();
        Falas = new List<ClassFalas>();
    }
}
