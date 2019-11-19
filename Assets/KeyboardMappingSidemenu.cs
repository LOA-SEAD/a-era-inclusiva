using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardMappingSidemenu : MonoBehaviour
{
    private Button[] buttons;

    private int _at = 0;
    // Start is called before the first frame update
    void Start()
    {
        buttons = GetComponentsInChildren<Button>().Where(x=>x.IsInteractable()).ToArray();
        buttons[0].Select();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (_at < buttons.Length-1)
            {
                _at++;
            } 
            buttons[_at].Select();
        } else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (_at > 0)
            {
                _at--;
            }
            buttons[_at].Select();
         
        }
    }
}
