using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Skill", order = 51)]
public class SkillContainer : ScriptableObject
{
    public SkillSet.Skill skill;
}
