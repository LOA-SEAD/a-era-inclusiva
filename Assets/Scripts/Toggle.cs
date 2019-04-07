using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Toggle : MonoBehaviour
{
    public TextMeshProUGUI nameObj;
    private Button _button;
   
    private void Awake()
    {
        _button = GetComponent<Button>();
    }
    
    public void AddListener(UnityAction action)
    {
        _button.onClick.AddListener(action);
    }
    
}