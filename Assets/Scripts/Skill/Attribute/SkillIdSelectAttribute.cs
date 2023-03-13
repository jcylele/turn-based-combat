using System;

namespace Skill.Attribute
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class SkillIdSelectAttribute : System.Attribute
    {
        public readonly Type idType;
        public SkillIdSelectAttribute(Type type)
        {
            idType = type;
        }
    }
}
