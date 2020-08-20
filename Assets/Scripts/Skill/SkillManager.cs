using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public List<SkillDisplay> skills;

    private void OnEnable()
    {
        SkillDisplay.OnSkillSelected += UnSelectSkills;
    }

    private void OnDisable()
    {
        SkillDisplay.OnSkillSelected -= UnSelectSkills;
    }

    void UnSelectSkills(int id)
    {
        skills.ForEach(x => x.UnSelect(id));
    }
}
