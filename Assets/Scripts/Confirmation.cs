using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Confirmation : MonoBehaviour
{

    public Button AcceptButton;
    public Button DenyButton;

    public TextMeshProUGUI Message;


    private void Awake()
    {
      
    }

    public void OnAccept(UnityAction x)
    {
        AcceptButton.onClick.RemoveAllListeners();
        AcceptButton.onClick.AddListener(x);
    }

    public void OnAccept(UnityAction x, bool clear)
    {
        if (clear) AcceptButton.onClick.RemoveAllListeners();
        AcceptButton.onClick.AddListener(x);
    }

    public void OnDeny(UnityAction x)
    {
        DenyButton.onClick.RemoveAllListeners();
        DenyButton.onClick.AddListener(x);
    }

    public void OnDeny(UnityAction x, bool clear)
    {
        if (clear) DenyButton.onClick.RemoveAllListeners();
        DenyButton.onClick.AddListener(x);
    }

    public void SetText(string text)
    {
        Message.SetText(text);
    }


    public void Hide()
    {
        gameObject.SetActive(false);
    }
}