using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillDisplay : MonoBehaviour, IInteractable
{
    [Header("Unlearnt colors")]
    [SerializeField]
    private Color unlearntIdleColor;
    [SerializeField]
    private Color unlearntHoverColor;
    
    [Space(10)]
    
    [Header("Learnt colors")]
    [SerializeField]
    private Color learntIdleColor;
    [SerializeField]
    private Color learntHoverColor;

    [Space(10)] 
    
    [Header("Border colors")]
    [SerializeField]
    private Color borderIdleColor;
    [SerializeField]
    private Color borderPressedColor;
    [SerializeField]
    private Color borderSelectColor;

    [Space(10)] 
    
    [SerializeField]
    private Image border;
    [SerializeField]
    private Text skillName;
    [SerializeField]
    private Text cost;
    [SerializeField]
    private SkillContainer currentSkill;

    public SkillContainer CurrentSkill => currentSkill;
    
    
    public static Action<int> OnSkillSelected = delegate(int id) {  };

    private bool selected = false;

    private int id;

    private SkillSet.Skill skillInfo => currentSkill.skill;
    private Image bgImage => GetComponent<Image>();
    
    void Awake()
    {
        skillName.text = skillInfo.skillName;
        cost.text = skillInfo.cost.ToString();
        bgImage.color = skillInfo.learnt ? learntIdleColor : unlearntIdleColor;
        border.color = borderIdleColor;
        id = skillInfo.id;
    }

    public void UnSelect(int _id)
    {
        if (_id == id)
            return;
        selected = false;
        bgImage.color = skillInfo.learnt ? learntIdleColor : unlearntIdleColor;
        border.color = borderIdleColor;
    }
    
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (selected) return;
        bgImage.color = skillInfo.learnt ? learntHoverColor : unlearntHoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (selected) return;
        bgImage.color = skillInfo.learnt ? learntIdleColor : unlearntIdleColor;
        border.color = borderIdleColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (selected) return;
        border.color = borderPressedColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (selected) return;
        OnSkillSelected(id);
        selected = true;
        border.color = borderSelectColor;
    }
}
