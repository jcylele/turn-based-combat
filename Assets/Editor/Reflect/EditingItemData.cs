using System;
using Skill.Skills;
using UnityEditor;

namespace Editor.Reflect
{
    /// <summary>
    /// tracker of current editing item
    /// </summary>
    public class EditingItemData
    {
        /// <summary>
        /// current item, target of Property and FieldInfo
        /// </summary>
        private BaseEditItem mItem;

        /// <summary>
        /// set item and property at the same time,
        /// in order to keep them synchronized
        /// </summary>
        public BaseEditItem Item
        {
            get => this.mItem;

            set => InnerSetItem(value);
        }

        private void InnerSetItem(BaseEditItem value)
        {
            this.mItem = value;
            if (this.property != null)
            {
                this.property.managedReferenceValue = value;
                this.property.serializedObject.ApplyModifiedProperties();    
            }
        }

        /// <summary>
        /// bound to object, can get and set value directly,
        /// keeps full path from the SerializedObject
        /// </summary>
        public SerializedProperty property;

        /// <summary>
        /// static info(independent from object),
        /// keeps only one layer relationship
        /// </summary>
        public Type fieldType;

        public Type BaseType => fieldType;

        public Type CurType => Item == null ? BaseType : Item.GetType();

        /// <summary>
        /// dive into one field
        /// </summary>
        public EditingItemData Forward(SerializedProperty newProperty)
        {
            var newItem = newProperty.managedReferenceValue as BaseEditItem;
            
            var newField = this.CurType.GetField(newProperty.name);
            var newFieldType = newField.FieldType;
            if (newFieldType.IsList())
            {
                newFieldType = newFieldType.GenericTypeArguments[0];
            }
            
            var ret = new EditingItemData
            {
                property = newProperty,
                fieldType = newFieldType,
                Item = newItem
            };

            return ret;
        }
    }
}