namespace Skill.Action
{
    public abstract class BaseTickAction : BaseAction
    {
        public new BaseTickActionData ActionData => actionData as BaseTickActionData;

        public abstract void OnEnter();

        public abstract void OnUpdate(int curFrame);

        public abstract void OnExit();

        public override void Update(int curFrame)
        {
            if (curFrame == this.ActionData.enterFrame)
            {
                this.OnEnter();
            }
            if (curFrame >= this.ActionData.enterFrame
                && curFrame <= this.ActionData.exitFrame)
            {
                this.OnUpdate(curFrame);
            }
            if (curFrame == this.ActionData.exitFrame)
            {
                this.OnExit();
            }
        }
    }
}