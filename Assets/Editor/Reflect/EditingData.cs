using Skill.Skills;
using System;

namespace Skill.Reflect
{
    /// <summary>
    /// current editing item
    /// </summary>
    public abstract class EditingItemData
    {
        public BaseEditItem Item
        {
            get;
            set;
        }

        public abstract Type BaseType { get; }
    }

    public class EditingIdItem : EditingItemData
    {
        public Type baseType;
        public int originalId;

        public override Type BaseType => baseType;
    }

    public class EditingFieldData : EditingItemData
    {
        [NonSerialized]
        public EditFieldInfo fieldInfo;

        public override Type BaseType => this.fieldInfo.FieldType;
    }
}
