using UnityEngine;

namespace Skill.Action
{
    public class PlayEffectAction : BaseTriggerAction
    {
        public new PlayEffectActionData ActionData => actionData as PlayEffectActionData;

        protected override void OnTrigger()
        {
            //Instantiate effect
            Debug.Log($"Play {this.ActionData.effectPath} At {this.ActionData.playPos}");
        }

        public override void OnInit()
        {
            //Load Prefab
            Debug.Log("OnInit");
        }

        public override void OnClose()
        {
            // Destroy Effect Instance
            Debug.Log("OnClose");
        }
    }
}
