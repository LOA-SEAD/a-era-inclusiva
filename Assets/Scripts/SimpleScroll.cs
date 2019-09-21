using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleScroll : MonoBehaviour
{
    protected int childrenCount;
    public Button DownButton;
    private Vector3 localPosition;
    private int _maxShown;
    private Vector3 newPosition;
    public GameObject parent;
    private float spacing = 10;
    private float step;
    public Button UpButton;
    public bool showScroll;


    protected void Awake()
    {
#if UNITY_STANDALONE
        _maxShown =3;
#else
        _maxShown = 3;
#endif
        spacing = GetComponentInChildren<VerticalLayoutGroup>().spacing;
        UpdateChildrenCount();
        localPosition = parent.transform.localPosition;
        newPosition = localPosition;
        step = (parent.GetComponent<RectTransform>().rect.height + spacing) / _maxShown;
        if (UpButton != null)
            UpButton.onClick.AddListener(delegate { GoUp(); });
        if (DownButton != null)
            DownButton.onClick.AddListener(delegate { GoDown(); });
    }

    public void BackToTop()
    {
        localPosition = Vector3.zero;
        newPosition = Vector3.zero;
        parent.transform.localPosition = localPosition;
    }

    public void Clear()
    {
        foreach (Transform child in parent.transform)
        {
            DestroyImmediate(child.gameObject);
        };
        BackToTop();
        UpdateChildrenCount();
    }

    private void GoDown()
    {
        if (newPosition.y + step >= (childrenCount - 3) * step) return;
        newPosition.y += step;
        StopAllCoroutines();
        StartCoroutine(AnimateMove());
    }

    private void GoUp()
    {
        if (newPosition.y - step <= -1 * step) return;
        newPosition.y -= step;
        StopAllCoroutines();
        StartCoroutine(AnimateMove());
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
        childrenCount = parent.transform.GetComponentsInChildren<Button>().Length;
        if (!showScroll)
            return;
        if (childrenCount > _maxShown)
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