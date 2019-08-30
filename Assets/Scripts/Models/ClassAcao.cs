using System;
using System.Collections.Generic;

[Serializable]
public class ClassAcao
{
    public string icone;
    public int id;
    public string nome;
    public bool selected;
    public string tipo;

    public void ToggleSelection()
    {
        selected = !selected;
    }
}
