using Skill.Attribute;
using Skill.Buffs;
using System;

namespace Skill.Skills
{
    [Skill]
    [SkillChild(1, typeof(BaseIdItem))]
    [SkillChild(2, typeof(BaseNoIdItem))]
    [Serializable]
    public abstract class BaseEditItem
    {
        public override string ToString()
        {
            return $"{this.GetType().Name}({this.GetHashCode()})";
        }
    }

    [Skill]
    [SkillChild(1, typeof(BaseSkill))]
    [SkillChild(2, typeof(BaseBuff))]
    [Serializable]
    public abstract class BaseIdItem : BaseEditItem
    {
        public int id;
    }

    [Skill]
    [SkillChild(1, typeof(BaseSkillRange))]
    [Serializable]
    public abstract class BaseNoIdItem : BaseEditItem
    {

    }
}
