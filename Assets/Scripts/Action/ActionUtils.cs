using Action.ActionData;
using Action.Actions;
using Action.Base;
using Skill.Action;

namespace Action
{
    public static class ActionUtils
    {
        public static BaseAction CreateAction(BaseActionData data)
        {
            switch (data)
            {
                case MoveActionData moveActionData:
                    {
                        var moveAction = new MoveAction();
                        moveAction.Bind(moveActionData);
                        return moveAction;
                    }
                case PlayEffectActionData playEffectActionData:
                    {
                        var playEffectAction = new PlayEffectAction();
                        playEffectAction.Bind(playEffectActionData);
                        return playEffectAction;
                    }
                default:
                    throw new System.Exception($"Unrecognized data {data}");
            }
        }
    }
}
