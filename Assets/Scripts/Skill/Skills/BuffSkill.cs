using Skill.Attribute;
using Skill.Buffs;
using System;

namespace Skill.Skills
{
    [Combat]
    [Serializable]
    [CombatShow("Cast A Buff")]
    class BuffSkill : BaseSkill
    {
        [CombatIdSelect(typeof(BaseBuff))]
        public int BuffID;

        public int lastTime;

        public bool canClear;
    }
}
