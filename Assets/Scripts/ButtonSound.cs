using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    // TODO: ao invés de ficar checando se disable = true, talvez remover o evento

    private Button button;
    public bool disable;

    private void Start()
    {
        disable = false;
        button = gameObject.GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(button.interactable && !disable)
            AudioManager.instance.PlaySfx((int)SoundType.ButtonHover, gameObject.transform.position.x, gameObject.transform.position.y);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (button.interactable && !disable)
            AudioManager.instance.PlaySfx((int)SoundType.ButtonClick, gameObject.transform.position.x);
    }

}
