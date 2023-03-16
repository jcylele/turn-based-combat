using System;

namespace Skill.Attribute
{
    /// <summary>
    /// indicates the subclasses of this class
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class CombatChildAttribute : System.Attribute
    {
        public readonly int priority;
        public readonly Type childType;
        public CombatChildAttribute(int priority, Type type)
        {
            this.priority = priority;
            childType = type;
        }
    }
}
