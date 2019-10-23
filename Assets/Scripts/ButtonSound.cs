using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public bool disable;
    private Button button;

    private void Start()
    {
        disable = false;
        button = gameObject.GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(button.interactable && !disable)
            AudioManager.instance.PlaySfx((int)SoundType.ButtonHover);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (button.interactable && !disable)
            AudioManager.instance.PlaySfx((int)SoundType.ButtonClick);
    }

}
