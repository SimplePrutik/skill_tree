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
    private SkillSet.Skill skillInfo => currentSkill.skill;


    public static Action<int> OnSkillSelected = delegate(int id) {  };

    private bool selected = false;
    public bool Selected => selected;

    private int id;
    public int Id => id;

    private bool learnt;
    public bool Learnt => learnt;

    private Image bgImage => GetComponent<Image>();
    
    void Awake()
    {
        //Transfer data from scriptable object
        skillName.text = skillInfo.skillName;
        cost.text = skillInfo.cost.ToString();
        learnt = skillInfo.Learnt;
        bgImage.color = learnt ? learntIdleColor : unlearntIdleColor;
        border.color = borderIdleColor;
        id = skillInfo.id;

        //Select base skill
        if (id == 0)
        {
            OnSkillSelected(id);
            selected = true;
            border.color = borderSelectColor;
        }
    }

    /// <summary>
    /// Remove selection from skill if it isn't the selected one
    /// </summary>
    /// <param name="_id"></param>
    public void UnSelect(int _id)
    {
        if (_id == id)
            return;
        selected = false;
        bgImage.color = learnt ? learntIdleColor : unlearntIdleColor;
        border.color = borderIdleColor;
    }

    /// <summary>
    /// Turn skill into learnt one
    /// </summary>
    public void Learn()
    {
        learnt = true;
        bgImage.color = learntHoverColor;
    }
    /// <summary>
    /// Turn skill into unlearnt one
    /// </summary>
    public void Forget()
    {
        learnt = false;
        bgImage.color = selected ? unlearntHoverColor : unlearntIdleColor;
    }
    
    
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (selected) return;
        bgImage.color = learnt ? learntHoverColor : unlearntHoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (selected) return;
        bgImage.color = learnt ? learntIdleColor : unlearntIdleColor;
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
