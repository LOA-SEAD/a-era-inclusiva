using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BarraInferior : MonoBehaviour
{
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI happinessIcon;
    private List<string> happinessIcons;

    void Start()
    {
 
        happinessIcons = new List<string>
        {
            "\uf556","\uf57a","\uf11a","\uf118","\uf59a"
        };
        pointsText.SetText(Game.Points.ToString());
        InvokeRepeating("UpdateHappinessIcon", 0, 1.0f);
    }

    public void IncrementScore(int quantity)
    {
        StopAllCoroutines();
        StartCoroutine(_incrementScore(quantity));
    }

    private void UpdateHappinessIcon()
    {
        int id = Game.Happiness / 25;
        float t = Game.Happiness / 100.0f;
        happinessIcon.SetText(happinessIcons[id]);
        happinessIcon.color = Color.Lerp(Color.red,Color.green, t);

    }

    private IEnumerator _incrementScore(int quantity)
    {
        var oldPoints = Game.Points;
        Game.Points += quantity;
        for (float timer = 0; timer < 0.5f; timer += Time.deltaTime)
        {
            float progress = timer / 0.5f;
            oldPoints = (int) Mathf.Lerp(oldPoints, Game.Points, progress);
            pointsText.SetText(oldPoints.ToString());
            yield return null;
        }

        pointsText.SetText(Game.Points.ToString());
    }
}