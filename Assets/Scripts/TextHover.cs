using System.Collections;
using TMPro;
using UnityEngine;

public class TextHover : MonoBehaviour
{
    public TextMeshProUGUI text;

    public float sizeAfterHover = 1.2f;

    public float defaultSize =1;
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
        while (!Mathf.Approximately(text.fontScale,to))
        {
            text.fontSize = Mathf.Lerp(text.fontScale, to, 0.1f);
            yield return new WaitForSeconds(0.01f);

        }
    }
    
}
