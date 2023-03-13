using Skill.Attribute;
using System;
using System.ComponentModel;

namespace Skill.Skills
{
    [Flags]
    public enum EAttackFlag
    {
        [Description("无")]
        None = 0,
        [Description("必定命中")]
        NoMiss = 1 << 0,
        [Description("必定暴击")]
        Critical = 1 << 1,
        [Description("忽视护甲")]
        IgnoreArmor = 1 << 2,
    }

    [Skill]
    [Serializable]
    class SpecialAttackSkill : AttackSkill
    {
        public EAttackFlag AttackFlag;
    }
}
