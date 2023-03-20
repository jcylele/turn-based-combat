using Skill.Attribute;
using Skill.Skills;
using System;
using System.ComponentModel;

namespace Skill.Buffs
{
    [Combat]
    [CombatChild(1, typeof(PropBuff))]
    [CombatChild(2, typeof(TickBuff))]
    [Serializable]
    [DisplayName("Buff")]
    public abstract class BaseBuff : BaseIdItem
    {

    }
}
