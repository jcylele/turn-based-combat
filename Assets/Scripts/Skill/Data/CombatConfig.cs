using Skill.Buffs;
using Skill.Skills;
using System.Collections.Generic;
using UnityEngine;

namespace Skill.Data
{
    [CreateAssetMenu(fileName = "CombatConfig")]
    public class CombatConfig : ScriptableObject
    {
        [SerializeReference]
        public List<BaseSkill> skills = new List<BaseSkill>();
        [SerializeReference]
        public List<BaseBuff> buffs = new List<BaseBuff>();
    }
}
