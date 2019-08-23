using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InputConfirmation : Confirmation
{
    public TMP_InputField InputField;

    private void Awake()
    {
        InputField.ActivateInputField();
        InputField.Select();
        
    }
}
