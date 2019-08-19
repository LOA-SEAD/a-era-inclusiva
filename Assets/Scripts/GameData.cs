public class GameData
{
    public ClassAcoes Actions;
    public ClassPersonagens Characters;


    public int Day;
    public ClassDemandas Demands;
    public int Happiness;
    public int[] LevelDemandingStudents;
    public int Points;
    public ClassResources Resources;
    public ClassAlunos Students;
    public int UrgenciaMinima;

    public GameData()
    {
        Happiness = 100;
        UrgenciaMinima = 2;
        Points = 0;
        Students = new ClassAlunos();
        Demands = new ClassDemandas();
        Actions = new ClassAcoes();
        Characters = new ClassPersonagens();
        Resources = new ClassResources();
        LevelDemandingStudents = new[] {4, 12, 17};
    }

    public GameData(SaveData saveData)
    {
        UrgenciaMinima = 2;
        Points = saveData.Points;
        Day = saveData.Day;
        Students = new ClassAlunos();
        Demands = new ClassDemandas();
        Actions = new ClassAcoes();
        Characters = new ClassPersonagens();
        Resources = new ClassResources();
        LevelDemandingStudents = new[] {4, 12, 17};
    }
}