using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SkillSet _skills;

    private int _skillPoints;

    void Init()
    {
        _skills = new SkillSet();
        _skillPoints = 0;
    }

    /// <summary>
    /// Skill point increment
    /// </summary>
    public void SkillPointsInc()
    {
        ++_skillPoints;
        Debug.Log($"PlayerInfo: Skill points - {_skillPoints}");
    }

    /// <summary>
    /// Learn skill with given id
    /// </summary>
    /// <param name="id"></param>
    void LearnSkill(int id)
    {
        var learnt = false;
        if (_skills.GetSkillInfo(id).cost <= _skillPoints)
            learnt =  _skills.Learn(id);
        
        if (learnt)
        {
            _skillPoints -= _skills.GetSkillInfo(id).cost;
            Debug.Log($"PlayerInfo: Skill {_skills.GetSkillInfo(id).skillName} has been learnt; Skill points - {_skillPoints}");
        }
        else
        {
            //Throw message
        }
    }

    /// <summary>
    /// Forget skill with given id
    /// </summary>
    /// <param name="id"></param>
    void ForgetSkill(int id)
    {
        var forgotten = _skills.Forget(id);
        if (forgotten)
        {
            _skillPoints += _skills.GetSkillInfo(id).cost;
        }
        else
        {
            //Throw message
        }
    }
}