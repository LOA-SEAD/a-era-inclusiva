using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SolutionPanel : MonoBehaviour
{
    public static string textTemplate = @"{0}<br><size=20>Solução: {1}</size>";
    public List<Image> stars;
    public static Dictionary<int, int> starByPoints = new Dictionary<int, int>() {{100, 3}, {50, 2}, {25, 1}, {0, 0}};

    public TextMeshProUGUI text;
    public void SetStars(int starCount)
    {

        // lembrar que o i que entra sao pontos, nao estrelas
        while (starCount > 0)
        {
            stars[starCount-1].color = Color.white;
            starCount--;
        }
    }

    public void SetText(string demand, string action)
    {
        text.SetText(string.Format(textTemplate, demand, action));
    }

   
}
