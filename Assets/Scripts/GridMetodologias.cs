using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GridMetodologias : MonoBehaviour
{
    public GameObject ActionPrefab;
    // Start is called before the first frame update
    void Start()
    {
        foreach(var action in Game.Actions.acoes)
        {
            var actionObj = Instantiate(ActionPrefab, transform);
            if (action.selected) {
                actionObj.GetComponent<Toggle>().isOn = true;
            }
            actionObj.GetComponentInChildren<Text>().text = action.nome;
            actionObj.GetComponent<Acao>().acao = action;
            actionObj.transform.SetParent(transform);

        }
    }
}
