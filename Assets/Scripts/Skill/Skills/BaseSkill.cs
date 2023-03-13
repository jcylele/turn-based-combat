using Skill.Attribute;
using System;

namespace Skill.Skills
{
    [Skill]
    [SkillChild(1, typeof(AttackSkill))]
    [SkillChild(2, typeof(BuffSkill))]
    [Serializable]
    public abstract class BaseSkill : BaseIdItem
    {
        public string name;
        public string desc;
    }
}
