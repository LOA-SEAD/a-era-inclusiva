using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimpleScroll : MonoBehaviour
{
    public float spacing = 10;
    public int maxShown = 3;
    private float step;
    public Button UpButton;
    public Button DownButton;
    public GameObject parent;
    private int childrenCount;
    private Vector3 localPosition;
    private Vector3 newPosition;


    protected void Awake()
    {
        UpdateChildrenCount();
        localPosition = parent.transform.localPosition;
        newPosition = localPosition;

        UpButton.onClick.AddListener(delegate { GoUp(); });
        DownButton.onClick.AddListener(delegate { GoDown(); });
        step = (parent.GetComponent<RectTransform>().rect.height+spacing) / maxShown;
    }

    public void BackToTop()
    {
        localPosition = Vector3.zero;
        newPosition = Vector3.zero;
        parent.transform.localPosition = localPosition;
    }

    public void Clear()
    {
        foreach (Transform child in parent.transform) {
            Destroy(child.gameObject);
        }

        BackToTop();
    }
    private void GoDown()
    {
        if (newPosition.y + step >= (childrenCount - 1) * step) return;
        newPosition.y += step;
        StopAllCoroutines();
        StartCoroutine(AnimateMove());
    }

    private void GoUp()
    {
        if (newPosition.y - step <= -2 * step) return;
        newPosition.y -= step;
        StopAllCoroutines();
        StartCoroutine(AnimateMove());
    }


    IEnumerator AnimateMove()
    {

        while (localPosition.y - newPosition.y < 0.01f || localPosition.y - newPosition.y > -0.01f)
        {
            localPosition = Vector3.Lerp(localPosition, newPosition, Time.deltaTime * 10);
            parent.transform.localPosition = localPosition;

            yield return null;
        }

        parent.transform.localPosition = newPosition;
    }

    public void UpdateChildrenCount()
    {
        childrenCount = parent.transform.childCount;
    }

    public void Add(GameObject _gameObject)
    {
        _gameObject.transform.SetParent(parent.transform);
        _gameObject.transform.SetAsLastSibling();
        _gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, step-spacing);
        UpdateChildrenCount();
        
    }

    public void AddList(List<GameObject> _gameObjects)
    {
        foreach (var obj in _gameObjects)
        {
            obj.transform.SetParent(parent.transform);
            obj.transform.SetAsLastSibling();  
            obj.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, step-spacing);
        }
        UpdateChildrenCount();
    }


}