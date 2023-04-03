using System;
using Action.ActionData;
using Skill.Attribute;

namespace Action.Base
{
    [Combat]
    [CombatChild(1, typeof(MoveActionData))]
    [Serializable]
    public class BaseTickActionData : BaseActionData
    {
        public int enterFrame;
        public int exitFrame;
    }
}
