using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollAlunos : MonoBehaviour
{
    public Button Up;
    public Button Down;
    public List<GameObject> Objects;
    public int MaxShown = 3;
    private int at = 0;
    // Start is called before the first frame update
    void Start()
    {
        Up.onClick.AddListener(GoUp);
        Down.onClick.AddListener(GoDown);
        UpdateShown();
       
    }

    void UpdateShown()
    {
        Objects.ForEach(x => x.SetActive(false));
        for(int id = at; id<at+MaxShown; id++)
        {
            Objects[id].SetActive(true);
        }
    }
    void GoUp()
    {
        if(at==0)
        {
            return;
        }
        at--;
        UpdateShown();
    }
    void GoDown() {
        if (at + MaxShown >= Objects.Count)
        {
            return;
        }
        at++;
        UpdateShown();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
