using Boo.Lang;

[System.Serializable]
public class ClassResource
{
    public string name;
    public string src;
    public string type;
    public string category;
}

public class ClassResources
{
    public List<ClassResource> resources;
    public ClassResources()
    {
        resources = new List<ClassResource>();
    }
}