using Skill.Attribute;
using Skill.Skills;
using System;

namespace Skill.Buffs
{
    [Combat]
    [CombatChild(1, typeof(PropBuff))]
    [CombatChild(2, typeof(TickBuff))]
    [Serializable]
    [CombatShow("BUFF")]
    public abstract class BaseBuff : BaseIdItem
    {

    }
}
