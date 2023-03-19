using System;
using UnityEngine;

namespace Skill.Action
{
    [Serializable]
    public class MoveActionData : BaseTickActionData
    {
        public Vector3 fromPosition;
        public Vector3 toPosition;
    }
}