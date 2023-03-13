using Skill.Attribute;
using Skill.Buffs;
using System;

namespace Skill.Skills
{
    [Skill]
    [Serializable]
    class BuffSkill : BaseSkill
    {
        [SkillIdSelect(typeof(BaseBuff))]
        public int BuffID;

        public int lastTime;

        public bool canClear;
    }
}
