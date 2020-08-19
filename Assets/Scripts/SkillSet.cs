
using System.Collections.Generic;
using System.Linq;

public class SkillSet
{
    class Skill
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
        public bool learnt;

    }

    private List<Skill> skillSet;

    public SkillSet()
    {
        //здесь может быть че угодно, инициализация из JSON, с сервера итд, для простоты пусть будет так
        skillSet = new List<Skill>
        {
            new Skill() {id = 0, skillName = "Base", cost = 0, skillDependencies = new List<int>{}, learnt = true},
            new Skill() {id = 1, skillName = "Jump", cost = 1, skillDependencies = new List<int>{0}, learnt = false},
            new Skill() {id = 2, skillName = "Double Jump", cost = 3, skillDependencies = new List<int>{1}, learnt = false},
            new Skill() {id = 3, skillName = "Shoot", cost = 2, skillDependencies = new List<int>{0}, learnt = false},
            new Skill() {id = 4, skillName = "Speak", cost = 2, skillDependencies = new List<int>{0}, learnt = false},
            new Skill() {id = 5, skillName = "Diplomacy", cost = 5, skillDependencies = new List<int>{3, 4}, learnt = false},
            new Skill() {id = 6, skillName = "Respect", cost = 10, skillDependencies = new List<int>{2, 5}, learnt = false},
        };
    }

    private Skill GetSkillInfo(int id)
    {
        return skillSet.Find(x => x.id == id);
    }

    public void Learn(int id)
    {
        if (GetSkillInfo(id).skillDependencies.Any(x => GetSkillInfo(x).learnt))
            skillSet[skillSet.FindIndex(x => x.id == id)].learnt = true;
    }
    
    public void Forget(int id)
    {
        if (skillSet.Where(x => x.learnt)
                    .All(y => y.skillDependencies.All(z => z != id)))
            skillSet[skillSet.FindIndex(x => x.id == id)].learnt = false;
    }
}
