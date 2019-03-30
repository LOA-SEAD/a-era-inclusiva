using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Game
{
    public static ClassAlunos Students;
    public static ClassDemandas Demands;
    public static ClassAcoes Actions;
    public static ClassPersonagens Characters;
    public static int Happiness;
    public static int LevelCounter = 0;
    public static int[] LevelDemandingStudents;

    public static void Setup()
    {
        Happiness = 0;
        Students = new ClassAlunos();
        Demands = new ClassDemandas();
        Actions = new ClassAcoes();
        Characters = new ClassPersonagens();
        LevelDemandingStudents = new int[] {4,12,17};
    }

    public static List<ClassAluno> DemandingStudents
    {
        get { return Students.alunos.FindAll(x => LevelDemandingStudents.ToList().Contains(x.id)); }
    }
    
}
