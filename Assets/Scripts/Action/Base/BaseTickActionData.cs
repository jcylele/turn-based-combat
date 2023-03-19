using System;

namespace Skill.Action
{
    [Serializable]
    public class BaseTickActionData : BaseActionData
    {
        public int enterFrame;
        public int exitFrame;
    }
}
