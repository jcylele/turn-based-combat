using System;
using System.Linq;
using Skill.Base;
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

            set
            {
                this.mItem = value;
                if (this.Property == null) return;
                this.Property.managedReferenceValue = value;
                this.Property.serializedObject.ApplyModifiedProperties();
            }
        }

        /// <summary>
        /// bound to object, can get and set value directly,
        /// keeps full path from the SerializedObject
        /// </summary>
        public SerializedProperty Property { get; }

        /// <summary>
        /// static info(independent from object),
        /// keeps only one layer relationship
        /// </summary>
        public Type FieldType { get; }

        public Type CurType => Item == null ? FieldType : Item.GetType();

        /// <summary>
        /// dive into one field
        /// </summary>
        public EditingItemData(SerializedProperty newProperty)
        {
            this.Property = newProperty;
            this.FieldType = newProperty.ManagedPropertyType();
            this.mItem = newProperty.managedReferenceValue as BaseEditItem;
        }
    }
}