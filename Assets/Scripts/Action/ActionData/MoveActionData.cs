using System;
using Action.Base;
using UnityEngine;

namespace Action.ActionData
{
    [Serializable]
    public class MoveActionData : BaseTickActionData
    {
        public Vector3 fromPosition;
        public Vector3 toPosition;
    }
}