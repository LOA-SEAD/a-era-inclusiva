using System.Collections;
using System.Collections.Generic;
using Coffee.UIExtensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StarPanel : MonoBehaviour
{
    public TextMeshProUGUI noPoints;
    public List<GameObject> starList;
    // Start is called before the first frame update

    public IEnumerator ShowStars(int starsQuantity, float waitTime)
    {
        noPoints.gameObject.SetActive(false);
        starList.ForEach(x=>x.SetActive(false));

        if (starsQuantity == 0)
        {
            noPoints.gameObject.SetActive(true);
            yield break;
        }
        for (int i=0; i<starsQuantity && i<starList.Count; i++)
        {
            starList[i].SetActive(true);
            starList[i].GetComponent<UIShiny>().Play();
            yield return new WaitForSeconds(waitTime);
        }
    }
}
