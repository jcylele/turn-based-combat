using System;
using Skill.Attribute;

namespace Action.Base
{
    [Combat]
    [CombatChild(1, typeof(BaseTickActionData))]
    [CombatChild(2, typeof(BaseTriggerActionData))]
    [Serializable]
    public abstract class BaseActionData
    {

    }
}
