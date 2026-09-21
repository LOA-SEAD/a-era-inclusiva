using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BotaoDemandaHTPI : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TextMeshProUGUI tick;
    public TextMeshProUGUI exclamation;
    public PairVisualTag pairVisualTag;

    private ClassDemanda _demanda;

    public ClassDemanda Demanda
    {
        set
        {
            _demanda = value;

            if (text != null)
            {
                text.SetText(_demanda.descricao);
            }

            if (exclamation != null)
            {
                exclamation.SetText(new string('\uf12a', _demanda.nivelUrgencia));
            }

            ClearPair();
        }

        get => _demanda;
    }

    public void Select()
    {
        if (tick != null)
        {
            tick.alpha = 1f;
        }
    }

    public void Select(int pairNumber)
    {
        Select();

        if (pairVisualTag != null)
        {
            pairVisualTag.SetPairNumber(pairNumber);
        }
    }

    public void ClearPair()
    {
        if (tick != null)
        {
            tick.alpha = 0f;
        }

        if (pairVisualTag != null)
        {
            pairVisualTag.ClearPair();
        }
    }
}