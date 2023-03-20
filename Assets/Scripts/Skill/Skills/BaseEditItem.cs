using Skill.Attribute;
using Skill.Buffs;
using System;
using UnityEngine.EventSystems;

namespace Skill.Skills
{
    [Combat]
    [CombatChild(1, typeof(BaseIdItem))]
    [CombatChild(2, typeof(BaseNoIdItem))]
    [Serializable]
    public abstract class BaseEditItem
    {
    }

    [Combat]
    [CombatChild(1, typeof(BaseSkill))]
    [CombatChild(2, typeof(BaseBuff))]
    [Serializable]
    public abstract class BaseIdItem : BaseEditItem, IComparable<BaseIdItem>
    {
        [CombatShow("unique id", "use different partition for each base type")]
        public int id;

        public string name;
        public string desc;

        public override string ToString()
        {
            return $"{this.name}({this.id})";
        }

        public int CompareTo(BaseIdItem other)
        {
            return this.id - other.id;
        }
    }

    [Combat]
    [CombatChild(1, typeof(BaseSkillRange))]
    [CombatChild(2, typeof(EventTrigger))]
    [Serializable]
    public abstract class BaseNoIdItem : BaseEditItem
    {
        public override string ToString()
        {
            return $"{this.GetType().Name}({this.GetHashCode()})";
        }
    }
}