using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarraInferior : MonoBehaviour
{   
     public TextMeshProUGUI weekText;
    public TextMeshProUGUI happinessIcon;
    private List<string> happinessIcons;
    public TextMeshProUGUI pointsText;
    public float levelTimeInSeconds = 180f;
    public ControladorSalaDeAula csd;
    private float totalTime;
    public Image timerFill;
    private bool ended;

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
        pointsText.SetText(GameManager.PlayerData.Points.ToString());
        weekText.SetText("Semana "+GameManager.PlayerData.Day.ToString());
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
            if ( !ended && levelTimeInSeconds <= 0)
            {
                ended = true;
                csd.End();
            }

            timerFill.fillAmount = levelTimeInSeconds/totalTime;
    }
    

    public void UpdateHappinessIcon()
    {
        var id = GameManager.PlayerData.Happiness / 25;
        var t = GameManager.PlayerData.Happiness / 100.0f;
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