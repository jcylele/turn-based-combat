using Skill.Attribute;
using System;
using UnityEngine;

namespace Skill.Skills
{
    [Skill]
    [SkillChild(1, typeof(SpecialAttackSkill))]
    [Serializable]
    public class AttackSkill : BaseSkill
    {
        public int AttackFactor;

        public int AttackFix;

        [SerializeReference]
        public BaseSkillRange AttackRange;
    }
}
