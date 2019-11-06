using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeechBubble : MonoBehaviour
{
    private static readonly int ShowHash = Animator.StringToHash("show");

    private readonly Dictionary<int, string> answers = new Dictionary<int, string>
    {
        [100] = "Acho que isso deu muito certo!",
        [50] = "Não parece ser o ideal, mas resolve o problema por hora",
        [25] = "Eu sei que consigo fazer melhor que isso!",
        [0] = "Acho que isso não funcionou muito bem",
        [-1] = "Acho que isso não funcionou muito bem"

    };

    public TextMeshProUGUI textMesh;

    public void ShowResult(int points)
    {
        GetComponent<Animator>().SetTrigger(ShowHash);
        SetText(answers[points]);
    }

    public void SetText(string text)
    {
        if (text != textMesh.text)
            GetComponent<Animator>().SetTrigger(ShowHash);
        textMesh.SetText(text);
    }
}