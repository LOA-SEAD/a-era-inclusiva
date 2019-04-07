using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ScrollList : MonoBehaviour
{
    public Button up;
    public Button down;
    public List<GameObject> objects;
     public int maxShown = 3;
     public Transform objectsParent;
    private int _at;

    // Start is called before the first frame update
    protected void Awake()
    {
        up.onClick.AddListener(GoUp);
        down.onClick.AddListener(GoDown);
     
    }

    protected void Start()
    {
        if (objects == null)
        {
            objects = new List<GameObject>();
        }
        UpdateShown();
    }
    public void AddGameObject(Transform obj)
    {
        obj.SetParent(objectsParent);
        obj.SetAsLastSibling();
        objects.Add(obj.gameObject);
    }


    protected void UpdateShown()
    {
    
            objects.ForEach(x => x.SetActive(false));
            for (int id = _at; id < _at + maxShown && id<objects.Count; id++)
            {
                    objects[id].transform.localScale = Vector3.one;
                    objects[id].SetActive(true);
            }
    }

    protected void GoUp()
    {
        if (_at == 0)
        {
            return;
        }

        _at--;
        UpdateShown();
    }

    protected void GoDown()
    {
        if (_at + maxShown >= objects.Count)
        {
            return;
        }

        _at++;
        UpdateShown();
   
       
    }

    public void Clear()
    {
        _at = 0;
        objects.RemoveAll(x=>x);
        foreach (Transform children in objectsParent.transform)
        {
            Destroy(children.gameObject);
        }
    }

    // Update is called once per frame
}