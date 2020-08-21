using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SkillSet _skills;

    private int _skillPoints;

    private int skillSelectedId;

    void Awake()
    {
        _skills = new SkillSet();
        _skillPoints = 0;
        OnTextBoxChanged("skill_points", _skillPoints.ToString());
        SkillSelectHandler(0);
    }

    public static Action<string, bool> OnButtonEnabled = delegate(string buttonName, bool enabled) { };
    public static Action<string, string> OnTextBoxChanged = delegate(string buttonName, string value) { };

    private void OnEnable()
    {
        SkillDisplay.OnSkillSelected += SkillSelectHandler;
        SkillManager.OnSkillLearnt += LearnSkill;
        Button.OnButtonPressed += ButtonHandler;
    }
    
    
    private void OnDisable()
    {
        SkillDisplay.OnSkillSelected -= SkillSelectHandler;
        SkillManager.OnSkillLearnt -= LearnSkill;
        Button.OnButtonPressed -= ButtonHandler;
    }
    
    void ButtonHandler(string buttonName)
    {
        switch (buttonName)
        {
            case "gain":
                SkillPointsInc();
                break;
            case "learn":
                LearnSkill(skillSelectedId);
                break;
            case "forget":
                ForgetSkill(skillSelectedId);
                break;
            case "forget_all":
                ForgetAllSkills();
                break;
        }
    }

    /// <summary>
    /// Synchronize UI with current data
    /// </summary>
    private void UIChanged()
    {
        OnButtonEnabled("learn", _skills.IsLearnable(skillSelectedId) && _skillPoints >= _skills.GetSkillInfo(skillSelectedId).cost);
        OnButtonEnabled("forget", _skills.IsForgetable(skillSelectedId));
        OnTextBoxChanged("cost", _skills.GetSkillInfo(skillSelectedId).cost.ToString());
        OnTextBoxChanged("skill_points", _skillPoints.ToString());
    }


    /// <summary>
    /// Handles selection change
    /// </summary>
    /// <param name="id"></param>
    private void SkillSelectHandler(int id)
    {
        var skill = _skills.GetSkillInfo(id);
        if (skill == null) return;
        skillSelectedId = skill.id;
        UIChanged();
    }

    /// <summary>
    /// Skill point increment
    /// </summary>
    public void SkillPointsInc()
    {
        ++_skillPoints;
        UIChanged();
    }

    /// <summary>
    /// Learn skill with given id
    /// </summary>
    /// <param name="id"></param>
    void LearnSkill(int id)
    {
        if (_skills.GetSkillInfo(id).cost <= _skillPoints)
        {
            _skillPoints -= _skills.Learn(id);
            UIChanged();
        }
    }

    /// <summary>
    /// Forget skill with given id
    /// </summary>
    /// <param name="id"></param>
    void ForgetSkill(int id)
    {
        var returned_pts = _skills.Forget(id);
        _skillPoints += returned_pts;
        UIChanged();
    }

    void ForgetAllSkills()
    {
        var returned_pts = _skills.ForgetAll();
        _skillPoints += returned_pts;
        UIChanged();
    }
}