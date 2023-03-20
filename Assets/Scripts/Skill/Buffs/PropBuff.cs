using Skill.Attribute;
using System;
using UnityEngine;

namespace Skill.Buffs
{
    public enum EProp
    {
        [InspectorName("Strength")]
        STR = 0,
        [InspectorName("Dexterity")]
        DEX = 1,
        [InspectorName("Intelligence")]
        INT = 2,
        [InspectorName("Invalid")]
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
