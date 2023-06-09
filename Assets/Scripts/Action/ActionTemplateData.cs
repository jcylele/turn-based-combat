﻿using System.Collections.Generic;
using Action.Base;
using UnityEngine;

namespace Skill.Action
{
    public class ActionTemplateData : ScriptableObject
    {
        public string templateName;

        public int totalFrame;

        [NonReorderable] [SerializeReference] public List<BaseActionData> actionDataList;
    }
}