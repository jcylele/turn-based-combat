using Action.ActionData;
using Action.Base;
using UnityEngine;

namespace Action.Actions
{
    public class MoveAction : BaseTickAction
    {
        public new MoveActionData ActionData => actionData as MoveActionData;

        public override void OnEnter()
        {
            Debug.Log($"OnEnter");
        }

        public override void OnUpdate(int curFrame)
        {
            float t = ((float)(curFrame - this.ActionData.enterFrame)) / (this.ActionData.exitFrame - this.ActionData.enterFrame);
            Vector3 curPos = Vector3.Lerp(this.ActionData.fromPosition, this.ActionData.toPosition, t);
            Debug.Log($"{curFrame}: {curPos}");
        }

        public override void OnExit()
        {
            Debug.Log($"OnExit");
        }
    }
}