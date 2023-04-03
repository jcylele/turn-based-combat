using System;
using Action.Base;
using UnityEngine;

namespace Action.ActionData
{
    [Serializable]
    public class PlayEffectActionData : BaseTriggerActionData
    {
        public string effectPath;
        public Vector3 playPos;
    }
}
