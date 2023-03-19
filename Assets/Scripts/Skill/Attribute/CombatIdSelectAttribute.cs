using System;
using UnityEngine;

namespace Skill.Attribute
{
    /// <summary>
    /// decorate int field， indicates that the value is id of an instance of specific type and its sub types)
    /// <para>the editor will show a dropdown instead of an input field</para>
    /// </summary>
    public class CombatIdSelectAttribute : PropertyAttribute
    {
        public readonly Type idType;
        public CombatIdSelectAttribute(Type type)
        {
            idType = type;
        }
    }
}
