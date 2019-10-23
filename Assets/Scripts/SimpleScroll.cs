using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleScroll : MonoBehaviour
{
    public int childrenCount = 0;
    public int _at = 0;
    public Button DownButton;
    private Vector3 localPosition;
    private int _maxShown = 3;
    private Vector3 newPosition;
    public GameObject parent;
    private float spacing = 10;
    private float step;
    public Button UpButton;
    public bool showScroll;
    public event EventHandler TopReached;
    public event EventHandler BottomReached;

    protected void Awake()
    {

        spacing = GetComponentInChildren<VerticalLayoutGroup>().spacing;
        UpdateChildrenCount();
        localPosition = parent.transform.localPosition;
        newPosition = localPosition;
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

        localPosition = Vector3.zero;
        newPosition = Vector3.zero;
        parent.transform.localPosition = localPosition;
    }

    public void Clear()
    {
        _at = 0;
        for (var i = parent.transform.childCount- 1; i >= 0; i--)
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
        if (_at >= childrenCount - 3)
        {
            BottomReached?.Invoke(this, EventArgs.Empty);

            return;
        }
        newPosition.y += step;
        StopAllCoroutines();
        StartCoroutine(AnimateMove());
        _at++;
    }

    public void GoUp()
    {

        if (_at <= 0)
        {
            TopReached?.Invoke(this, EventArgs.Empty);
            return;
        }
        newPosition.y -= step;
        StopAllCoroutines();
        StartCoroutine(AnimateMove());
        _at--;
    }


    private IEnumerator AnimateMove()
    {
        while (Mathf.Abs(localPosition.y - newPosition.y) > 1.0f)
        {
            localPosition = Vector3.Lerp(localPosition, newPosition, Time.deltaTime * 10);
            parent.transform.localPosition = localPosition;

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
    }
}