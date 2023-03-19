using Skill.Attribute;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;

namespace Skill.Action
{
    [CustomPropertyDrawer(typeof(CombatIdSelectAttribute))]
    public class IdSelectDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var idSelectAttribute = attribute as CombatIdSelectAttribute;

            if (property.propertyType != SerializedPropertyType.Integer)
            {
                throw new Exception("CombatIdSelectAttribute can only be used on int fields");
            }

            var field = new PopupField<int>();
            field.label = property.displayName ?? property.name;
            //field.choices = this.m_DataStore.GetIdList(editFieldInfo.IDSelectType);
            field.choices = new List<int>() { 1, 2, 3, 4, 5 };
            field.value = property.intValue;
            field.RegisterCallback((ChangeEvent<int> evt) =>
            {
                property.intValue = evt.newValue;
            });

            return field;
        }
    }
}
