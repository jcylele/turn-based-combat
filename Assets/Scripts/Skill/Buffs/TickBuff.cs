using Skill.Attribute;
using System;

namespace Skill.Buffs
{
    [Serializable]
    [CombatShow("Ticker Buff", "buff which triggers periodically and does come stuff")]
    public class TickBuff : BaseBuff
    {
        public int interval;
    }
}
