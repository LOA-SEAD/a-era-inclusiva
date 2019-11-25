using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FeedbackController : MonoBehaviour
{
    public HTPIController htpiController;
    public SceneController sceneController;
    public Dictionary<ClassDemanda, int> results;

    public Confirmation confirmation;


    public void Setup()
    {
        results = new Dictionary<ClassDemanda, int>();
        foreach (var resolucao in htpiController._resolucoes)
        {
            results[resolucao.Key] = resolucao.Key.EfficiencyOf(resolucao.Value);
        }

    }

    public void ShowConfirmation(object sender, EventArgs eventArgs)
    {
        confirmation.Message.SetText("Deseja finalizar o dia?");
        confirmation.AcceptButton.onClick.AddListener(()=>sceneController.ChangeTo("Scenes/FinalAula"));
        confirmation.DenyButton.onClick.AddListener(()=>confirmation.gameObject.SetActive(false));
        confirmation.gameObject.SetActive(true);

    }

    


}