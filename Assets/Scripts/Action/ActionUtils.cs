namespace Skill.Action
{
    public class ActionUtils
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
