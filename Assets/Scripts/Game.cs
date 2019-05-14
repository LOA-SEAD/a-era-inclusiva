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
    public static ClassResources Resources;
    private static int _happiness;
    public static int UrgenciaMinima;

    public static int Happiness
    {
        get { return _happiness; }
        set
        {
            if(value <= 100 && value >= 0)
                _happiness = value;
        }
    }

    public static int LevelCounter = 0;
    public static int Points;
    public static int[] LevelDemandingStudents;

    public static void Setup()
    {
        UrgenciaMinima = 2;
        Points = 0;
        Happiness = 100;
        Students = new ClassAlunos();
        Demands = new ClassDemandas();
        Actions = new ClassAcoes();
        Characters = new ClassPersonagens();
        Resources = new ClassResources();
        LevelDemandingStudents = new int[] {4,12,17};
    }

    public static List<ClassAluno> DemandingStudents
    {
        get { return Students.alunos.FindAll(x => LevelDemandingStudents.ToList().Contains(x.id)); }
    }

}
