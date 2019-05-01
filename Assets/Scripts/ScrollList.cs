using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ScrollList<T> : MonoBehaviour
{
    public Button up;
    public Button down;
    public List<GameObject> objects;
     public Transform objectsParent;
    private int selected;

    public int Selected
    {
        get
        {
            if (selected < 0) return 0;
            if (selected > objects.Count-1) return objects.Count()-1;
            return selected;
        }
    
        set
        {
            if (value < 0 || value > objects.Count-1)
                return;
            objects[Selected].GetComponent<Image>().color = (Color.white);
            selected = value;
            objects[Selected].GetComponent<Image>().color = (Color.black);

            for (int i = 0; i < objects.Count; i++)
            {
                if (i >= Selected-1 && i <= Selected + 1)
                {
                    objects[i].SetActive(true);
                }
                else
                {
                    objects[i].SetActive(false);
                }
            }

        }
    }

    // Start is called before the first frame update
    protected void Awake()
    {
        if(up!=null)
            up.onClick.AddListener(GoUp);
        if(down!=null)
            down.onClick.AddListener(GoDown);
     
    }

    protected void Start()
    {
        if (objects == null)
        {
            objects = new List<GameObject>();
        }
    }
    public void AddGameObject(Transform obj)
    {
        obj.SetParent(objectsParent);
        obj.SetAsLastSibling();
        objects.Add(obj.gameObject);
        obj.localScale = Vector3.one;
        Selected=objects.Count-1;
        
    }



    protected void GoUp()
    {
        Selected--;

    }

    protected void GoDown()
    {
        Selected++;


    }

    public void Clear()
    {
        Selected = 0;
        objects.RemoveAll(x=>x);
        foreach (Transform children in objectsParent.transform)
        {
            Destroy(children.gameObject);
        }
    }
    protected virtual void OnSelect(T obj)
    {
     
    }

    // Update is called once per frame
}