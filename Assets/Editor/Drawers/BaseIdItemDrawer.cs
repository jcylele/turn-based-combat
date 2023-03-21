using Editor.SkillEditor;
using Skill.Base;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Editor.Drawers
{
    // [CustomPropertyDrawer(typeof(BaseIdItem), true)]
    public class BaseIdItemDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var propertyField = new PropertyField(property);
            // var canEdit = property.serializedObject.targetObject.GetType() == typeof(EditingCombatConfig);
            // propertyField.SetEnabled(canEdit);

            return propertyField;
        }
    }
}