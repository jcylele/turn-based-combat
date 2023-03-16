using Skill.Buffs;
using Skill.Skills;
using System.Collections.Generic;
using UnityEngine;

namespace Skill.Data
{
    /// <summary>
    /// contains all combat configs
    /// </summary>
    [CreateAssetMenu(fileName = "CombatConfig")]
    public class CombatConfig : ScriptableObject
    {
        [SerializeReference]
        public List<BaseSkill> skills = new List<BaseSkill>();
        [SerializeReference]
        public List<BaseBuff> buffs = new List<BaseBuff>();
    }
}
