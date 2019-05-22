using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ActionListSalaDeAula : ActionList
{
    public ControladorSalaDeAula controller;
    protected override bool WhichActions(ClassAcao x)
    {
        return x.tipo == Type & x.selected;
    }

    protected override void OnSelect(ClassAcao action)
    {
        controller.UseAction(action);
    }
}
