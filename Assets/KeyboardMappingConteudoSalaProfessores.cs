using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardMappingConteudoSalaProfessores : MonoBehaviour
{
    private int side = 0;
    private int _at_left = 0;

    private int tab = 0;

    public Button[] MenuButtons;
    // Start is called before the first frame update
    void Start()
    {
        MenuButtons[0].Select();
    }

    // Update is called once per frame
    void Update()
    {
        if (side == 0)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (_at_left < MenuButtons.Length - 1)
                {
                    _at_left++;
                }

                MenuButtons[_at_left].Select();
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (_at_left > 0)
                {
                    _at_left--;
                }

                MenuButtons[_at_left].Select();

            }
            else if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                side = 1;
            }
        }
        else
        { if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                side = 0;
                MenuButtons[_at_left].Select();

            }
        }
        
    }
}
