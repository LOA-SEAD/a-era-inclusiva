using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimpleScroll : MonoBehaviour
{
    public float step = 80;
    public Button UpButton;
    public Button DownButton;
    public GameObject parent;
    private int childrenCount;
    private Vector3 localPosition;
    private Vector3 newPosition;


    protected void Start()
    {
        UpdateChildrenCount();
        localPosition = parent.transform.localPosition;
        newPosition = localPosition;

        UpButton.onClick.AddListener(delegate { GoUp(); });
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
        foreach (Transform child in parent.transform) {
            Destroy(child.gameObject);
        }
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
        Debug.Log(newPosition);

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
        UpdateChildrenCount();
        
    }


}