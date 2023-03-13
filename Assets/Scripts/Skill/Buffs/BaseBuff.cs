using Skill.Attribute;
using Skill.Skills;
using System;

namespace Skill.Buffs
{
    [Skill]
    [SkillChild(1, typeof(PropBuff))]
    [SkillChild(2, typeof(TickBuff))]
    [Serializable]
    public abstract class BaseBuff : BaseIdItem
    {

    }
}
