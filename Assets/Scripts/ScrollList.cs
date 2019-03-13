using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ScrollList : MonoBehaviour
{
    [FormerlySerializedAs("Up")] public Button up;
    [FormerlySerializedAs("Down")] public Button down;
    [FormerlySerializedAs("Objects")] public List<GameObject> objects;
    [FormerlySerializedAs("MaxShown")] public int maxShown = 3;

    private int _at;

    // Start is called before the first frame update
    void Awake()
    {
        up.onClick.AddListener(GoUp);
        down.onClick.AddListener(GoDown);
        up.transform.SetAsFirstSibling();
        down.transform.SetAsLastSibling();
        if (objects == null)
        {
            objects = new List<GameObject>();
        }
    }

    void Start()
    {
        UpdateShown();
    }
    public void AddGameObject(Transform obj)
    {
        obj.SetParent(transform);
        obj.SetSiblingIndex(1);
        objects.Add(obj.gameObject);
    }


    void UpdateShown()
    {
    
            objects.ForEach(x => x.SetActive(false));
            for (int id = _at; id < _at + maxShown && id<objects.Count; id++)
            {
                    objects[id].SetActive(true);
            }
    }

    void GoUp()
    {
        if (_at == 0)
        {
            return;
        }

        _at--;
        UpdateShown();
    }

    void GoDown()
    {
        if (_at + maxShown >= objects.Count)
        {
            return;
        }

        _at++;
        UpdateShown();
    }

    // Update is called once per frame
}