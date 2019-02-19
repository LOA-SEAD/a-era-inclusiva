using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class TextHover : MonoBehaviour
{
    public TextMeshProUGUI text;

    public float sizeAfterHover;

    public float defaultSize;
    // Start is called before the first frame update
    public void OnPointerHover()
    {
        StopAllCoroutines();
        StartCoroutine(ChangeFontSize(sizeAfterHover));
    }

 
    public void OnPointerOut()
    {
        StopAllCoroutines();
        StartCoroutine(ChangeFontSize(defaultSize));    
    }
    
    private IEnumerator ChangeFontSize(float to)
    {
        while (!Mathf.Approximately(text.fontSize,to))
        {
            text.fontSize = Mathf.Lerp(text.fontSize, to, 0.1f);
            yield return new WaitForSeconds(0.01f);

        }
    }
    
}
