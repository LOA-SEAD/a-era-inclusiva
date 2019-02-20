using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acao: MonoBehaviour
{
    public ClassAcao acao;
    // Start is called before the first frame update
    public void OnChange(bool selected)
    {
        Debug.Log(selected);
        if (selected)
            Game.SelectedActions.Add(acao);
        else
            Game.SelectedActions.Remove(acao);

    }

  
}
