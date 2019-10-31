using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SolutionPanel : MonoBehaviour
{
    public static string textTemplate = @"{0}<br><size=20>{1}</size>";
    public List<Image> stars;

    public TextMeshProUGUI text;
    public void SetStars(int i)
    {
        // lembrar que o i que entra sao pontos, nao estrelas
        while (i > 0)
        {
            stars[i]
        }
    }

    public void SetText(string demand, string action)
    {
        text.SetText(string.Format(textTemplate, demand, action));
    }

   
}
