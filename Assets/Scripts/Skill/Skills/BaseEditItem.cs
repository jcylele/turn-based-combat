using Skill.Attribute;
using Skill.Buffs;
using System;

namespace Skill.Skills
{
    [Combat]
    [CombatChild(1, typeof(BaseIdItem))]
    [CombatChild(2, typeof(BaseNoIdItem))]
    [Serializable]
    public abstract class BaseEditItem
    {
        public override string ToString()
        {
            return $"{this.GetType().Name}({this.GetHashCode()})";
        }
    }

    [Combat]
    [CombatChild(1, typeof(BaseSkill))]
    [CombatChild(2, typeof(BaseBuff))]
    [Serializable]
    public abstract class BaseIdItem : BaseEditItem
    {
        [CombatShow("unique id", "use different partition for each base type")]
        public int id;
    }

    [Combat]
    [CombatChild(1, typeof(BaseSkillRange))]
    [Serializable]
    public abstract class BaseNoIdItem : BaseEditItem
    {

    }
}
