﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class ControladorSalaDeAula : MonoBehaviour
{
    private DemandToggle _selectedDemand;
    public TextMeshProUGUI pointsText;
    public SpeechBubble speechBubble;
    public BarraInferior barraInferior;
    public float decreaseHappinessRate = 1.0f;
    public int DemandCounter { get; set; }
    public float levelTimeInSeconds;
    public AudioSource audioSource;
    public AudioClip acertoClip;
    public DemandToggle SelectedDemand
    {
        set
        {
            _selectedDemand = value;
            Speak(_selectedDemand.Demand.descricao); 
        }
       
    }

    private void Start()
    {
        InvokeRepeating("DecreaseHappiness", 0, decreaseHappinessRate);
    }

    void Update()
    {
        //timer da fase
        levelTimeInSeconds -= Time.deltaTime;
        if(levelTimeInSeconds < 0) //Acaba a cena
            SceneManager.LoadScene("HTPI");
    }

    public void DecreaseHappiness()
    {
      // Debug.Log(string.Format("Felicidade = {0} - {1}", Game.Happiness, DemandCounter));
        Game.Happiness -= DemandCounter;
    }
    public void UseAction(ClassAcao action)
    {
        if (_selectedDemand == null)
        {
            Speak("Não posso fazer isso sem ter escolhido a demanda!");
            return;
        }
        
        Efetividade e = _selectedDemand.Demand.acoesEficazes.FirstOrDefault(x => x.idAcao == action.id);
        _selectedDemand.Demand.resolvida = true;
        if (e != null)
        {
            audioSource.clip = acertoClip;
            audioSource.Play();
            switch (e.efetividade)
            {
                case 100:
                    Speak("Acho que isso deu muito certo!");
                    break;
                case 50:
                    Speak("Não parece ser o ideal, mas resolve o problema por hora");
                    break;
                default:
                    Speak("Eu sei que consigo fazer melhor que isso!");
                    break;
            }
            barraInferior.IncrementScore(e.efetividade);
        }
        else
        {
            Speak("Acho que isso não funcionou muito bem!");

        }
        Destroy(_selectedDemand.gameObject);
        _selectedDemand = null;
        if (Game.Demands.demandas.FindAll(x => !x.resolvida).Count == 0)
        {
            FindObjectOfType<SceneController>().ChangeTo("Scenes/HTPI");
        }
    }

    public void Speak(string demandaDescricao)
    {
         speechBubble.gameObject.SetActive(true);
                speechBubble.SetText(demandaDescricao);
    }
}