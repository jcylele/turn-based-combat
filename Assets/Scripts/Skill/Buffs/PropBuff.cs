using Skill.Attribute;
using System;

namespace Skill.Buffs
{
    public enum EProp
    {
        [CombatShow("strength")]
        STR = 0,
        [CombatShow("dexterity")]
        DEX = 1,
        [CombatShow("intelligence")]
        INT = 2,
        [CombatShow("do not select")]
        MAX = 3,
    }

    [Serializable]
    [CombatShow("Property Buff", "Affects property of unit")]
    public class PropBuff : BaseBuff
    {
        public EProp prop;
        public int val;
    }
}
