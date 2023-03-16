using Skill.Attribute;
using System;

namespace Skill.Skills
{
    [Combat]
    [CombatChild(1, typeof(AttackSkill))]
    [CombatChild(2, typeof(BuffSkill))]
    [Serializable]
    [CombatShow("SKILL")]
    public abstract class BaseSkill : BaseIdItem
    {
        public string name;
        public string desc;
    }
}
