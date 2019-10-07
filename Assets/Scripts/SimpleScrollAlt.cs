using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleScrollAlt : MonoBehaviour
{
    private readonly int _maxShown = 3;
    public int _at;
    public int childrenCount;
    public Button DownButton;
    public GameObject parent;
    private float spacing = 10;
    private float step;
    public Button UpButton;
    public event EventHandler TopReached;
    public event EventHandler BottomReached;

    protected void Awake()
    {
        spacing = GetComponentInChildren<VerticalLayoutGroup>().spacing;
        UpdateChildrenCount();
        if (UpButton != null)
            UpButton.onClick.AddListener(GoUp);
        if (DownButton != null)
            DownButton.onClick.AddListener(GoDown);
    }

    private void Start()
    {
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
        for (var i = parent.transform.childCount - 1; i >= 0; i--)
        {
            childrenCount--;
            var child = parent.transform.GetChild(i);
            child.SetParent(null);
            child.gameObject.SetActive(false);
            DestroyImmediate(child.gameObject);
            Debug.Log(i);
        }

        BackToTop();
        UpdateChildrenCount();
    }

    public void GoDown()
    {
        if (_at + 1 >= childrenCount)
        {
            BottomReached?.Invoke(this, EventArgs.Empty);

            return;
        }

        parent.transform.GetChild(_at).GetComponent<Button>().interactable = true;
        StopAllCoroutines();
        StartCoroutine(AnimateMove());
        
        _at++;
        Debug.Log($"at{_at}, childcount{childrenCount}");
        var button = parent.transform.GetChild(_at).GetComponent<Button>();
        button.interactable = false;
        button.onClick?.Invoke();
    }

    public void GoUp()
    {
        if (_at - 1 < 0)
        {
            TopReached?.Invoke(this, EventArgs.Empty);
            return;
        }
        parent.transform.GetChild(_at).GetComponent<Button>().interactable = true;

        StopAllCoroutines();
        StartCoroutine(AnimateMove());
        _at--;
        Debug.Log($"at{_at}, childcount{childrenCount}");

        var button = parent.transform.GetChild(_at).GetComponent<Button>();
        button.interactable = false;

        button.onClick?.Invoke();
    }


    private IEnumerator AnimateMove()
    {
        var newPosition = Vector3.zero;

        newPosition.y = Mathf.Min(Mathf.Max(_at * step, 0), (childrenCount - 3) * step);
        while (Mathf.Abs(parent.transform.localPosition.y - newPosition.y) > 1.0f)
        {
            parent.transform.localPosition =
                Vector3.Lerp(parent.transform.localPosition, newPosition, Time.deltaTime * 10);

            yield return null;
        }

        parent.transform.localPosition = newPosition;
    }

    public virtual void UpdateChildrenCount()
    {
        if (childrenCount > 0)
        {
            if (UpButton != null)
                UpButton.gameObject.SetActive(true);
            if (DownButton != null)
                DownButton.gameObject.SetActive(true);
        }
        else
        {
            if (UpButton != null)
                UpButton.gameObject.SetActive(false);
            if (DownButton != null)
                DownButton.gameObject.SetActive(false);
        }
    }


    public void Add(GameObject _gameObject)
    {
        childrenCount++;
        step = (parent.GetComponent<RectTransform>().rect.height + spacing) / _maxShown;

        _gameObject.transform.SetParent(parent.transform);
        _gameObject.transform.SetAsLastSibling();
        _gameObject.GetComponent<RectTransform>()
            .SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, step - spacing);
        _gameObject.transform.localScale = Vector3.one;
        UpdateChildrenCount();
    }

    public void AddList(List<GameObject> _gameObjects)
    {
        childrenCount += _gameObjects.Count;
        step = (parent.GetComponent<RectTransform>().rect.height + spacing) / _maxShown;
        foreach (var obj in _gameObjects)
        {
            obj.transform.SetParent(parent.transform);
            obj.transform.SetAsLastSibling();
            obj.transform.localScale = Vector3.one;
            obj.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, step - spacing);
        }
        
        UpdateChildrenCount();
        var firstButton =parent.transform.GetChild(0).GetComponent<Button>();
        firstButton.interactable=false;
        firstButton.onClick?.Invoke();
    }
}