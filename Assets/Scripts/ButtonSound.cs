using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    private Button button;

    private void Start()
    {
        button = gameObject.GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(button.interactable)
            AudioManager.instance.PlaySfx((int)SoundType.ButtonHover);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (button.interactable)
            AudioManager.instance.PlaySfx((int)SoundType.ButtonClick);
    }

}
