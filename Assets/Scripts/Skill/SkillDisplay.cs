using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillDisplay : MonoBehaviour, IInteractable
{
    [Header("Unlearnt colors")]
    public Color unlearntIdleColor;
    public Color unlearntHoverColor;
    
    [Space(10)]
    
    [Header("Learnt colors")]
    public Color learntIdleColor;
    public Color learntHoverColor;

    [Space(10)] 
    
    [Header("Border colors")]
    public Color borderIdleColor;
    public Color borderPressedColor;
    public Color borderSelectColor;

    [Space(10)] 
    
    public Image border;
    public Text skillName;
    public Text cost;
    public SkillContainer skillContainer;

    private int id;

    private SkillSet.Skill skillInfo => skillContainer.skill;
    private Image bgImage => GetComponent<Image>();
    
    void Awake()
    {
        skillName.text = skillInfo.skillName;
        cost.text = skillInfo.cost.ToString();
        bgImage.color = skillInfo.learnt ? learntIdleColor : unlearntIdleColor;
        border.color = borderIdleColor;
        id = skillInfo.id;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        bgImage.color = skillInfo.learnt ? learntHoverColor : unlearntHoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        bgImage.color = skillInfo.learnt ? learntIdleColor : unlearntIdleColor;
        border.color = borderIdleColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        border.color = borderPressedColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        border.color = borderSelectColor;
    }
}
