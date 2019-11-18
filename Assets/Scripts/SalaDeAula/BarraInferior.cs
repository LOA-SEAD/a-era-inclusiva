using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarraInferior : MonoBehaviour
{
    private int _happiness;
    public TextMeshProUGUI happinessIcon;
    private List<string> happinessIcons;
    public TextMeshProUGUI pointsText;
    public float levelTimeInSeconds = 150f;
    public ControladorSalaDeAula csd;
    private float totalTime;
    public Image timerFill;
    private int Happiness
    {
        get => _happiness;
        set
        {
            _happiness = value;
            UpdateHappinessIcon();
        }
    }

    void Awake()
    {
        totalTime = levelTimeInSeconds;
        happinessIcons = new List<string>
        {
            "\uf556", "\uf57a", "\uf11a", "\uf118", "\uf59a"
        };
    }
    private void Start()
    {

     
        if (GameManager.PlayerData != null)
            Setup(this, EventArgs.Empty);
        else
        {
            SaveManager.DataLoaded += Setup;
        }
    }

    private void Setup(object sender, EventArgs eventArgs)
    {
        Happiness = GameManager.PlayerData.Happiness;
        UpdateHappinessIcon();
        pointsText.SetText(GameManager.PlayerData.Points.ToString());
    }


    public void IncrementScore(int quantity)
    {
        StopAllCoroutines();
        StartCoroutine(_incrementScore(quantity));
    }

    private void Update()
    {  
            //timer da fase
            levelTimeInSeconds -= Time.deltaTime;
            if (levelTimeInSeconds <= 0)
            {
                csd.End();
            }

            timerFill.fillAmount = levelTimeInSeconds/totalTime;
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
            oldPoints = (int)Mathf.Lerp(oldPoints, GameManager.PlayerData.Points, progress);
            pointsText.SetText(oldPoints.ToString());
            yield return null;
        }

        pointsText.SetText(GameManager.PlayerData.Points.ToString());
    }
}