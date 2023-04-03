namespace Action.Base
{
    public abstract class BaseTriggerAction : BaseAction
    {
        public new BaseTriggerActionData ActionData => actionData as BaseTriggerActionData;

        protected abstract void OnTrigger();

        public override void Update(int curFrame)
        {
            if (this.ActionData.triggerFrame == curFrame)
            {
                OnTrigger();
            }
        }
    }
}