using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.instance.PlaySfx((int)SoundType.ButtonHover);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.instance.PlaySfx((int)SoundType.ButtonClick);
    }

}
