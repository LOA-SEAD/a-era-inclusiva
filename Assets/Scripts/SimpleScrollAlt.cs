using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

public class SimpleScrollAlt : MonoBehaviour
{
    private readonly int _maxShown = 3;
    public bool InvokeOnClick;
    public bool Horizontal;
    public int _at;
    public List<Selectable> children;
    public Button DownButton;
    public GameObject parent;
    private float spacing = 10;
    private float step;
    public Button UpButton;
    
    public event EventHandler TopReached;
    public event EventHandler BottomReached;

    protected void Awake()
    {
        children = new List<Selectable>();
        spacing = GetComponentInChildren<HorizontalOrVerticalLayoutGroup>().spacing;
        if (UpButton != null)
            UpButton.onClick.AddListener(delegate { GoUp(); });
        if (DownButton != null)
            DownButton.onClick.AddListener(delegate { GoDown(); });
    }

    private void Start()
    {
        if (Horizontal)
            step = (parent.GetComponent<RectTransform>().rect.width + spacing) / _maxShown;
        else
            step = (parent.GetComponent<RectTransform>().rect.height + spacing) / _maxShown;
    }

    public void BackToTop()
    {
        _at = 0;

        parent.transform.localPosition = Vector3.zero;
    }

    public void Clear()
    {
        _at = 0;
        foreach (var child in children)
        {
            child.transform.SetParent(null);
            child.gameObject.SetActive(false);
            Destroy(child.gameObject);
        }

        children.Clear();
        BackToTop();
    }

    public bool GoDown()
    {
        if (_at + 1 >= children.Count)
        {
            BottomReached?.Invoke(this, EventArgs.Empty);

            return false;
        }


        children[_at + 1].GetComponent<Button>().onClick?.Invoke();
        StopAllCoroutines();
        StartCoroutine(AnimateMove());


        return true;
    }


    public bool GoUp()
    {
        if (_at - 1 < 0)
        {
            TopReached?.Invoke(this, EventArgs.Empty);
            return false;
        }

        children[_at - 1].GetComponent<Button>().onClick?.Invoke();

        StopAllCoroutines();
        StartCoroutine(AnimateMove());


        return true;
    }

    private void UpdateIndex(int index)
    {
        children[_at].interactable = true;
        _at = index;
        children[_at].interactable = false;
    }


    private IEnumerator AnimateMove()
    {
        var newPosition = Vector3.zero;
        if (Horizontal)
            newPosition.x = Mathf.Min(Mathf.Max(_at * step, 0), (children.Count - 3) * step);
        else
            newPosition.y = Mathf.Min(Mathf.Max(_at * step, 0), (children.Count - 3) * step);
        while (Vector3.Distance(parent.transform.localPosition, newPosition) > 1.0f)
        {
            parent.transform.localPosition =
                Vector3.Slerp(parent.transform.localPosition, newPosition, Time.deltaTime * 10);

            yield return null;
        }

        parent.transform.localPosition = newPosition;
    }


    public void Add(Selectable _gameObject)
    {
        children.Add(_gameObject);
        if (Horizontal)
            step = (parent.GetComponent<RectTransform>().rect.width + spacing) / _maxShown;
        else
            step = (parent.GetComponent<RectTransform>().rect.height + spacing) / _maxShown;

        _gameObject.transform.SetParent(parent.transform);
        _gameObject.transform.SetAsLastSibling();
        _gameObject.GetComponent<RectTransform>()
            .SetSizeWithCurrentAnchors(Horizontal ? RectTransform.Axis.Horizontal : RectTransform.Axis.Vertical,
                step - spacing);
        _gameObject.transform.localScale = Vector3.one;
        int index = children.Count - 1;
        _gameObject.GetComponent<Button>().onClick.AddListener(() => UpdateIndex(index));
    }

    public void AddList(List<Selectable> _gameObjects)
    {
        foreach (var obj in _gameObjects)
        {
            Add(obj);
        }

        UpdateIndex(0);
    }

    public void SelectLast()
    {
        children.Last().GetComponent<Button>().onClick?.Invoke();
    }

    public void SelectFirst()
    {
        children.First().GetComponent<Button>().onClick?.Invoke();
    }
}