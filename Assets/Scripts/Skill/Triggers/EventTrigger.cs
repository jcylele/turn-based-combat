using System;
using Skill.Skills;

namespace Skill.Triggers
{
    public enum ETiming
    {
        None = 0,
        Attack = 1,
        BeAttacked = 2,
        Die = 3,
    }

    public enum ERelation
    {
        Self = 1,
        Alliance = 1 << 1,
        Enemy = 1 << 2,
        Friend = Self | Alliance,
        All = Friend | Enemy,
    }
    
    [Serializable]
    public class EventTrigger : BaseNoIdItem
    {
        public ETiming triggerTiming;
        public ERelation triggerRelation;
    }
}