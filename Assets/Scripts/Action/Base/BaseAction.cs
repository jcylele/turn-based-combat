namespace Action.Base
{
    public abstract class BaseAction
    {
        protected BaseActionData actionData;

        public BaseActionData ActionData => actionData;

        public void Bind(BaseActionData data)
        {
            this.actionData = data;
        }

        public virtual void OnInit()
        {

        }

        public abstract void Update(int curFrame);

        public virtual void OnClose()
        {

        }
    }
}
