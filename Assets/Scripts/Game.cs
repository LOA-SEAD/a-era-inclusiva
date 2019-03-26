using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game
{
    public static ClassAlunos Students;
    public static ClassDemandas Demands;
    public static ClassAcoes Actions;
    public static ClassPersonagens Characters;
    public static int Happiness; 

    public static void Setup()
    {
        Happiness = 0;
        Students = new ClassAlunos();
        Demands = new ClassDemandas();
        Actions = new ClassAcoes();
        Characters = new ClassPersonagens();
    }
    

}
