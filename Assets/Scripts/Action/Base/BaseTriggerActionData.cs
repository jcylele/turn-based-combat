using System;

namespace Skill.Action
{
    [Serializable]
    public abstract class BaseTriggerActionData : BaseActionData
    {
        public int triggerFrame;
    }
}