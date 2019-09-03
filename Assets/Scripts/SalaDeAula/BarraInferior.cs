using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BarraInferior : MonoBehaviour
{
    private int _happiness;
    public TextMeshProUGUI happinessIcon;
    private List<string> happinessIcons;
    public TextMeshProUGUI pointsText;

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
            "\uf556", "\uf57a", "\uf11a", "\uf118", "\uf59a"
        };
        Happiness = GameManager.PlayerData.Happiness;
        UpdateHappinessIcon();
    }

    private void Start()
    {
        pointsText.SetText(GameManager.PlayerData.Points.ToString());
    }

    public void IncrementScore(int quantity)
    {
        StopAllCoroutines();
        StartCoroutine(_incrementScore(quantity));
    }

    private void Update()
    {
        if (Happiness != GameManager.PlayerData.Happiness) Happiness = GameManager.PlayerData.Happiness;
    }

    public void UpdateHappinessIcon()
    {
        var id = Happiness / 25;
        var t = Happiness / 100.0f;
        happinessIcon.SetText(happinessIcons[id]);
        happinessIcon.color = Color.Lerp(Color.red, Color.green, t);
    }

    private IEnumerator _incrementScore(int quantity)
    {
        var oldPoints = GameManager.PlayerData.Points;
        GameManager.PlayerData.Points += quantity;
        for (float timer = 0; timer < 0.5f; timer += Time.deltaTime)
        {
            var progress = timer / 0.5f;
            oldPoints = (int) Mathf.Lerp(oldPoints, GameManager.PlayerData.Points, progress);
            pointsText.SetText(oldPoints.ToString());
            yield return null;
        }

        pointsText.SetText(GameManager.PlayerData.Points.ToString());
    }
}