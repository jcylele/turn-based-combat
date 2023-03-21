using System.Linq;
using Editor.Reflect;
using Editor.SkillEditor;
using Skill.Attribute;
using Skill.Base;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Editor.Drawers
{
    [CustomPropertyDrawer(typeof(CombatIdSelectAttribute))]
    public class IdSelectDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var editingCombatConfig = property.serializedObject.targetObject as EditingCombatConfig;
            if (editingCombatConfig == null)
            {
                return new PropertyField(property);
            }

            var idSelectAttribute = (CombatIdSelectAttribute)attribute;
            var choices = editingCombatConfig.allData.GetFilteredDataList(idSelectAttribute.idType).ToList();
            var curIndex = choices.FindIndex((item => item.id == property.intValue));
            var popupField = new PopupField<BaseIdItem>(property.displayName, choices, curIndex, item =>
            {
                if (item == null)
                {
                    return string.Empty;
                }
                property.intValue = item.id;
                property.serializedObject.ApplyModifiedProperties();
                return item.ToString();
            });
            return popupField;
        }
    }
}