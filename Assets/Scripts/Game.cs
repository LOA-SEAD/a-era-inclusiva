using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game
{
    public static List<ClassAluno> Students;
    public static List<ClassDemanda> Demands;
    public static List<ClassAcao> Actions;
    public static List<ClassAcao> SelectedActions;
    public static ClassPersonagens Dialogs;
    public static int Happiness; 

    public static void Setup()
    {
        Happiness = 0;
        Students = new List<ClassAluno>();
        Demands = new List<ClassDemanda>();
        Actions = new List<ClassAcao>();
        SelectedActions = new List<ClassAcao>();
        Dialogs = new ClassPersonagens();
    }
}
