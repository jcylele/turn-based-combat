using System;

namespace Skill.Buffs
{
    public enum EProp
    {
        STR = 0,
        DEX = 1,
        INT = 2,
        MAX = 3,
    }

    [Serializable]
    public class PropBuff : BaseBuff
    {
        public EProp prop;
        public int val;
    }
}
