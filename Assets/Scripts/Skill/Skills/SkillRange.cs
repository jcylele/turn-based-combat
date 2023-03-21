using Skill.Attribute;
using System;
using Skill.Base;

namespace Skill.Skills
{
    [Combat]
    [CombatChild(1, typeof(SingleSkillRange))]
    [CombatChild(2, typeof(CircleSkillRange))]
    [CombatChild(3, typeof(RectSkillRange))]
    [Serializable]
    public abstract class BaseSkillRange : BaseNoIdItem
    {

    }

    [Combat]
    [Serializable]
    public class SingleSkillRange : BaseSkillRange
    {
        public int range;
    }

    [Combat]
    [Serializable]
    public class CircleSkillRange : BaseSkillRange
    {
        public int radius;
    }

    [Combat]
    [Serializable]
    public class RectSkillRange : BaseSkillRange
    {
        public int width;
        public int height;
    }
}
