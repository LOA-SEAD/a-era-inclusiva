using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BotaoDemandaHTPI : MonoBehaviour
{
    public TextMeshProUGUI text;

    public TextMeshProUGUI tick;
    public Image avatar;
    // Start is called before the first frame update
    public void SetDemand(ClassDemanda demanda)
    {
        text.SetText(demanda.descricao);
    }

    public void Select()
    {
        tick.alpha = 1f;
    }
    // Update is called once per frame
}
