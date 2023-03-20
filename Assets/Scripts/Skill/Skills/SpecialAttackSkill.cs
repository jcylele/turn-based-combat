using Skill.Attribute;
using System;

namespace Skill.Skills
{
    [Flags]
    public enum EAttackFlag
    {
        None = 0,
        NoMiss = 1 << 0,
        Critical = 1 << 1,
        IgnoreArmor = 1 << 2,
    }

    [Combat]
    [Serializable]
    [CombatShow("Attack With Effects")]
    class SpecialAttackSkill : AttackSkill
    {
        public EAttackFlag AttackFlag;
    }
}