using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour
{
    private int at;
    private int childCount;
    public List<Button> children;
    public Button Down;
    private float height;
    public GameObject parent;

    private int shown = 3;
    private int spacing = 10;

    public Button Up;

    // Start is called before the first frame update
    void Start()
    {
        height = (parent.GetComponent<RectTransform>().rect.height + spacing) / shown;
        at = 0;
        Down.onClick.AddListener(() =>
        {
           GoDown();
        });
        Up.onClick.AddListener(() =>
        {
            GoUp();
        });
    }

    public void GoDown()
    {
        if (at + 1 >= childCount) return;
        at++;
        children[at].Select();
    }

    public void GoUp()
    {
        if (at -1 < 0) return;
        at--;
        children[at].Select();
    }

    public void Add(Button _gameObject)
    {
        children.Add(_gameObject);
        height = (parent.GetComponent<RectTransform>().rect.height + spacing) / shown;

        _gameObject.transform.SetParent(parent.transform);
        _gameObject.transform.SetAsLastSibling();
        _gameObject.GetComponent<RectTransform>()
            .SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,height - spacing);
        _gameObject.transform.localScale = Vector3.one;
        int index = children.Count - 1;
        _gameObject.onClick.AddListener(() => UpdateIndex(index));
    }

    public void AddList(List<Button> _gameObjects)
    {
        foreach (var obj in _gameObjects)
        {
            Add(obj);
        }

        UpdateIndex(0);
    }

    public void SelectLast()
    {
        children.Last().Select();
    }

    public void SelectFirst()
    {
        children.First().Select();
    }

    private void UpdateIndex(int index)
    {
        at = index;
        children[at].Select();
        
    }
}
