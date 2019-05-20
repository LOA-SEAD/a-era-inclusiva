using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ActionListSalaDeAula : ActionList
{
    protected override bool WhichActions(ClassAcao x)
    {
        return x.tipo == Type & x.selected;
    }
}
