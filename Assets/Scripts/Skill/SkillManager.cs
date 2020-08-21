using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public List<SkillDisplay> skills;
    
    public static Action<int> OnSkillLearnt = delegate(int id) {  };
    public static Action<int> OnSkillForgotten = delegate(int id) {  };

    private void OnEnable()
    {
        SkillDisplay.OnSkillSelected += UnSelectSkills;
        Button.OnButtonPressed += ButtonHandler;
    }

    private void OnDisable()
    {
        SkillDisplay.OnSkillSelected -= UnSelectSkills;
        Button.OnButtonPressed -= ButtonHandler;
    }

    /// <summary>
    /// Unselect skills when selected a new one
    /// </summary>
    /// <param name="id"></param>
    void UnSelectSkills(int id)
    {
        skills.ForEach(x => x.UnSelect(id));
    }

    void ButtonHandler(string buttonName)
    {
        var skill = skills.Find(x => x.Selected);
        switch (buttonName)
        {
            case "learn":
                if (skill == null) return;
                skill.Learn();
                OnSkillLearnt(skill.Id);
                break;
            case "forget":
                if (skill == null) return;
                skill.Forget();
                OnSkillForgotten(skill.Id);
                break;
            case "forget_all":
                var skillsToForget = skills.Where(x => x.Learnt && x.Id != 0);
                foreach (var _skill in skillsToForget)
                {
                    _skill.Forget();
                    OnSkillForgotten(_skill.Id);
                }
                break;
        }
    }
}
