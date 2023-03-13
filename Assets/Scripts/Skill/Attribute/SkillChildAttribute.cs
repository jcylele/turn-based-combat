using System;

namespace Skill.Attribute
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class SkillChildAttribute : System.Attribute
    {
        public readonly int priority;
        public readonly Type childType;
        public SkillChildAttribute(int priority, Type type)
        {
            this.priority = priority;
            childType = type;
        }
    }
}
