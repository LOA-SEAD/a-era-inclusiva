using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeechBubble : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    private static readonly int ShowHash = Animator.StringToHash("show");

    public void SetText(string text)
    {
        if(text!=textMesh.text)
            GetComponent<Animator>().SetTrigger(ShowHash);
        textMesh.SetText(text);

    }
    // Start is called before the first frame update
  

    // Update is called once per frame
   
}
