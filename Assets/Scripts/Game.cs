using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game
{
    public static ClassAlunos Students;
    public static List<ClassDemanda> Demands;
    public static ClassAcoes Actions;
    public static ClassPersonagens Characters;
    public static int Happiness; 

    public static void Setup()
    {
        Happiness = 0;
        Students = new ClassAlunos();
        Demands = new List<ClassDemanda>();
        Actions = new ClassAcoes();
        Characters = new ClassPersonagens();
    }

}
