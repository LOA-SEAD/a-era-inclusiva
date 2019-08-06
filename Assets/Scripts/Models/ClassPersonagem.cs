using System.Collections.Generic;

[System.Serializable]
public class ClassPersonagem{
    public string nome;
    public List<ClassFala> dialogos;
}

[System.Serializable]
public class ClassPersonagens
{
    public List<ClassPersonagem> personagens;
    public ClassPersonagens()
    {
        personagens = new List<ClassPersonagem>();
    }
}