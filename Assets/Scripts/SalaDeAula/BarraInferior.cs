using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BarraInferior : MonoBehaviour
{
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI happinessIcon;
    private List<string> happinessIcons;

    private int _happiness;
    private int Happiness
    {
        get => _happiness;
        set
        {
            _happiness = value;
            UpdateHappinessIcon();
        }
    }

    private void Awake()
    {
        happinessIcons = new List<string>
        {
            "\uf556","\uf57a","\uf11a","\uf118","\uf59a"
        };
        Happiness = Game.Happiness;
        UpdateHappinessIcon();
    }
    void Start()
    {
        pointsText.SetText(Game.Points.ToString());
    }

    public void IncrementScore(int quantity)
    {
        StopAllCoroutines();
        StartCoroutine(_incrementScore(quantity));
    }

    void Update()
    {
        if (Happiness != Game.Happiness)
        {
            Happiness = Game.Happiness;
        }    
    }
    public void UpdateHappinessIcon()
    {
        int id = Happiness / 25;
        float t = Happiness / 100.0f;
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