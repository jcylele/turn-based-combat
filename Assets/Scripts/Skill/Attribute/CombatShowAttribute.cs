using System;

namespace Skill.Attribute
{
    /// <summary>
    /// change the display name and tooltip string of a class/field(enum value)
    /// TODO tooltip is not implemented yet
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Class, AllowMultiple = false)]
    public class CombatShowAttribute : System.Attribute
    {
        public readonly string showName;
        public readonly string showDesc;
        public CombatShowAttribute(string name, string showDesc = null)
        {
            this.showName = name;
            this.showDesc = showDesc;
        }
    }
}
