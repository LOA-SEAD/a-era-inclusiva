using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game
{
    public static List<ClassAluno> Students;
    public static List<ClassDemanda> Demands;
    public static ClassAcoes Actions;
    public static ClassPersonagens Characters;
    public static int Happiness; 

    public static void Setup()
    {
        Happiness = 0;
        Students = new List<ClassAluno>();
        Demands = new List<ClassDemanda>();
        Actions = new ClassAcoes();
        Characters = new ClassPersonagens();
    }

}
