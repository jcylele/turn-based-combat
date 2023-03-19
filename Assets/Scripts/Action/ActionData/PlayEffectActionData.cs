using System;
using UnityEngine;

namespace Skill.Action
{
    [Serializable]
    public class PlayEffectActionData : BaseTriggerActionData
    {
        public string effectPath;
        public Vector3 playPos;
    }
}
