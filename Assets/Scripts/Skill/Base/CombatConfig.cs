﻿using System.Collections.Generic;
using UnityEngine;

namespace Skill.Base
{
    /// <summary>
    /// contains all combat configs
    /// </summary>
    [CreateAssetMenu(fileName = "CombatConfig")]
    public class CombatConfig : ScriptableObject
    {
        [NonReorderable] [SerializeReference] public List<BaseIdItem> allData = default;
    }
}