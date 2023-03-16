using System;

namespace Skill.Attribute
{
    /// <summary>
    /// mark it as an editable class in skill editor
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class CombatAttribute : System.Attribute
    {
    }
}
