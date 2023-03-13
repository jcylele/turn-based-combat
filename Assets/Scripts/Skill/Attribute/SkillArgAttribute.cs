using System;

namespace Skill.Attribute
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SkillArgAttribute : System.Attribute
    {
        public readonly string name;
        public SkillArgAttribute(string name)
        {
            this.name = name;
        }
    }
}
