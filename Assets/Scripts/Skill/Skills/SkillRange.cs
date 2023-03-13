using Skill.Attribute;
using System;

namespace Skill.Skills
{
    [Skill]
    [SkillChild(1, typeof(SingleSkillRange))]
    [SkillChild(2, typeof(CircleSkillRange))]
    [SkillChild(3, typeof(RectSkillRange))]
    [Serializable]
    public abstract class BaseSkillRange : BaseNoIdItem
    {

    }

    [Skill]
    [Serializable]
    public class SingleSkillRange : BaseSkillRange
    {
        public int range;
    }

    [Skill]
    [Serializable]
    public class CircleSkillRange : BaseSkillRange
    {
        public int radius;
    }

    [Skill]
    [Serializable]
    public class RectSkillRange : BaseSkillRange
    {
        public int width;
        public int height;
    }
}
