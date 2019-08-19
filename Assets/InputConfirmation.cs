using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class InputConfirmation : Confirmation
{
    public TMP_InputField InputField;
    public UnityAction<string> acao;

    private void Start()
    {
       OnAccept(delegate {  acao.Invoke(InputField.text); });
    }
    // Start is called before the first frame update
    
    // Update is called once per frame
   
}
