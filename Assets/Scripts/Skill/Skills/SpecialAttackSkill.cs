using Skill.Attribute;
using System;

namespace Skill.Skills
{
    //TODO Multiple Selection DropDown is not supported yet
    //TODO CombatShow of Enum values
    [Flags]
    public enum EAttackFlag
    {
        [CombatShow("none")]
        None = 0,
        [CombatShow("must hit the target")]
        NoMiss = 1 << 0,
        [CombatShow("must be critical")]
        Critical = 1 << 1,
        [CombatShow("ignore armor of target")]
        IgnoreArmor = 1 << 2,
    }

    [Combat]
    [Serializable]
    [CombatShow("Attack With Effects")]
    class SpecialAttackSkill : AttackSkill
    {
        [CombatShow("Special Flags")]
        public EAttackFlag AttackFlag;
    }
}
