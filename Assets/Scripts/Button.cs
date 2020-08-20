using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Button : MonoBehaviour, IInteractable
{
    [Header("State Colors")] 
    [SerializeField]
    private Color enabledColor;
    [SerializeField]
    private Color disabledColor;
    [SerializeField]
    private Color hoverColor;
    [SerializeField]
    private Color pressedColor;
    
    private Image bg => GetComponent<Image>();

    private bool interactionEnabled = true;

    public string buttonName;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!interactionEnabled) return;
        bg.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!interactionEnabled) return;
        bg.color = enabledColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!interactionEnabled) return;
        bg.color = pressedColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!interactionEnabled) return;
        bg.color = enabledColor;
    }

    public void SetInteractionMode(bool enable)
    {
        interactionEnabled = enable;
        bg.color = enable ? enabledColor : disabledColor;
    }
}