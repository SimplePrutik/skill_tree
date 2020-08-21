
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillSet
{
    [Serializable]
    public class Skill
    {
        /// <summary>
        /// Skill ID
        /// </summary>
        public int id;
        /// <summary>
        /// Skill name
        /// </summary>
        public string skillName;
        /// <summary>
        /// How many skillpoints does it require to be learnt
        /// </summary>
        public int cost;
        /// <summary>
        /// Which skills provide an ability to be learnt
        /// </summary>
        public List<int> skillDependencies;
        /// <summary>
        /// Is it already learnt
        /// </summary>
        private bool learnt;
        public bool Learnt => learnt;

        public Skill(int id, string skillName, int cost, List<int> skillDependencies, bool learnt)
        {
            this.id = id;
            this.skillName = skillName;
            this.cost = cost;
            this.skillDependencies = new List<int>(skillDependencies);
            this.learnt = learnt;
        }

        public void SetLearn(bool _learnt) => learnt = _learnt;
    }

    private readonly List<Skill> _skillSet;

    public SkillSet()
    {
        //здесь может быть че угодно, инициализация из JSON, с сервера итд, для простоты пусть будет так
        //так же можно сделать расширение для Editor, чтоб он генерировал ScriptableObject по входящим данным
        _skillSet = new List<Skill>
        {
            new Skill(0, "Base", 0, new List<int>{}, true),
            new Skill(1, "Jump", 1, new List<int>{0}, false),
            new Skill(2, "Double Jump", 3, new List<int>{1}, false),
            new Skill(3, "Shoot", 2, new List<int>{0}, false),
            new Skill(4, "Speak", 2, new List<int>{0}, false),
            new Skill(5, "Diplomacy", 5, new List<int>{3, 4}, false),
            new Skill(6, "Respect", 10, new List<int>{2, 5}, false),
        };
    }

    /// <summary>
    /// Get skill from skillset
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Skill GetSkillInfo(int id)
    {
        var skill = _skillSet.Find(x => x.id == id);
        if (skill == null)
            Debug.LogWarning("Incorrect skill ID");
        return skill;
    }

    
    public bool IsLearnable(int id) => !GetSkillInfo(id).Learnt && GetSkillInfo(id).skillDependencies.Any(x => GetSkillInfo(x).Learnt);
    public bool IsForgetable(int id) => id != 0 && GetSkillInfo(id).Learnt && _skillSet.Where(x => x.Learnt)
                                                .All(y => y.skillDependencies.All(z => z != id));

    /// <summary>
    /// Learns skill if possible and returns amount of wasted points 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public int Learn(int id)
    {
        if (IsLearnable(id))
        {
            var _skill = _skillSet[_skillSet.FindIndex(x => x.id == id)];
            _skill.SetLearn(true);
            return _skill.cost;
        }

        return 0;
    }
    
    /// <summary>
    /// Forgets skill if possible and returns amount of returned points
    /// </summary>
    /// <param name="id"></param>
    /// <param name="forced"></param>
    /// <returns></returns>
    public int Forget(int id, bool forced = false)
    {
        if (IsForgetable(id) || (forced && GetSkillInfo(id).Learnt && id != 0))
        {
            Debug.Log($"Skill forgotten - {GetSkillInfo(id).skillName}");
            var _skill = _skillSet[_skillSet.FindIndex(x => x.id == id)];
            _skill.SetLearn(false);
            return _skill.cost;
        }

        return 0;
    }

    /// <summary>
    /// Forgets all skills and returns amount of returned points
    /// </summary>
    /// <returns></returns>
    public int ForgetAll()
    {
        var pts = 0;
        foreach (var skill in _skillSet)
            pts += Forget(skill.id, true);
        return pts;
    }
    
}
