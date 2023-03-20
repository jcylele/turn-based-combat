using Skill.Attribute;
using System;
using UnityEngine;

namespace Skill.Skills
{
    [Combat]
    [CombatChild(1, typeof(SpecialAttackSkill))]
    [Serializable]
    [CombatShow("Normal Attack")]
    public class AttackSkill : BaseSkill
    {
        public int AttackFactor;

        public int AttackFix;

        [SerializeReference] public BaseSkillRange AttackRange;
    }
}