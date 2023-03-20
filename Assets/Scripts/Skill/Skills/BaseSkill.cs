using Skill.Attribute;
using System;
using System.Collections.Generic;
using Skill.Triggers;
using UnityEngine;

namespace Skill.Skills
{
    [Combat]
    [CombatChild(1, typeof(AttackSkill))]
    [CombatChild(2, typeof(BuffSkill))]
    [Serializable]
    public abstract class BaseSkill : BaseIdItem
    {
        [SerializeReference] public List<EventTrigger> triggers;
    }
}