using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Confirmation : MonoBehaviour
{
    public Button DenyButton;

    public Button AcceptButton;

    public TextMeshProUGUI Message;

    public TextMeshProUGUI Title;


    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
         gameObject.SetActive(false);   
        }
        
    }


    public void OnAccept(UnityAction x)
    {
        
       AcceptButton.onClick.RemoveAllListeners();
       AcceptButton.onClick.AddListener(x);
    }
    public void OnDeny(UnityAction x)
    {
        
        DenyButton.onClick.RemoveAllListeners();
        DenyButton.onClick.AddListener(x);
    }

    public void SetText(string text)
    {
        Message.SetText(text);
    }

    public void SetTitle(string title)
    {
        Title.SetText(title);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    
}