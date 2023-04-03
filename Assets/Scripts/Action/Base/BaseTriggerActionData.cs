using System;
using Action.ActionData;
using Skill.Attribute;

namespace Action.Base
{
    [Combat]
    [CombatChild(1, typeof(PlayEffectActionData))]
    [Serializable]
    public abstract class BaseTriggerActionData : BaseActionData
    {
        public int triggerFrame;
    }
}