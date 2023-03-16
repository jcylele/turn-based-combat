using System;

namespace Skill.Attribute
{
    /// <summary>
    /// decorate int field， indicates that the value is id of an instance of specific type and its sub types)
    /// <para>the editor will show a dropdown instead of an input field</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class CombatIdSelectAttribute : System.Attribute
    {
        public readonly Type idType;
        public CombatIdSelectAttribute(Type type)
        {
            idType = type;
        }
    }
}
