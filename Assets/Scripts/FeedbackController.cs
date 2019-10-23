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

    private List<Tuple<ClassDemanda, ClassAcao, int>> results;

    private int _at = 0;

    public TextMeshProUGUI demandText;
    public StarPanel starPanel;

    public TextMeshProUGUI actionText;

    public Confirmation confirmation;
    // Start is called before the first frame update
    public Dictionary<int, int> starByPoints = new Dictionary<int, int>() {{100, 3}, {50, 2}, {25, 1}, {0, 0}};
public void Setup()
    {
        results = new List<Tuple<ClassDemanda, ClassAcao, int>>();
        foreach (var resolucao in htpiController._resolucoes)
        {
            var eficaz = resolucao.Key?.acoesEficazes.FirstOrDefault(x=>x.idAcao== resolucao.Value.id);
            var points = 0;
            if (eficaz != null)
            {
                points = eficaz.efetividade;
            }
          

            results.Add(new Tuple<ClassDemanda, ClassAcao, int>(resolucao.Key, resolucao.Value, points));
        }

        SetResult();
    }

    private void SetResult()
    {
        starPanel.StopAllCoroutines();
        demandText.SetText(results[_at].Item1.descricao);
        actionText.SetText(results[_at].Item2.icone +" "+ results[_at].Item2.nome);
        starPanel.StartCoroutine(starPanel.ShowStars(starByPoints[results[_at].Item3], 0.3f));

    }

    public void GoToNext()
    {
        if (_at == results.Count - 1)
        {
            confirmation.gameObject.SetActive(true);
            return;
        }

        _at++;
        SetResult();
    }

    public void GoToBefore()
    {
        if (_at == 0)
        {
            return;
        }
        _at--;
        SetResult();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
