public class GameData
{
    public ClassAcoes Actions;
    public ClassPersonagens Characters;
    
    public ClassDemandas Demands;
    public int[] LevelDemandingStudents;
    public ClassResources Resources;
    public ClassAlunos Students;
    public int UrgenciaMinima = 2;

    public GameData()
    {

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
        Students = new ClassAlunos();
        Demands = new ClassDemandas();
        Actions = new ClassAcoes();
        Characters = new ClassPersonagens();
        Resources = new ClassResources();
        LevelDemandingStudents = new[] {4, 12, 17};
    }
}