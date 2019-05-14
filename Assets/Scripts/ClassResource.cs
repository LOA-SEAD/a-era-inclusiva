using System.Collections.Generic;

[System.Serializable]
public class ClassResource
{
    public string name;
    public string src;
    public string type;
    public string category;
}
[System.Serializable]
public class ClassResources
{
    public List<ClassResource> resources;
    public ClassResources()
    {
        resources = new List<ClassResource>();
    }
}