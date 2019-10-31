using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackController : MonoBehaviour
{
    public HTPIController htpiController;

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

    


}