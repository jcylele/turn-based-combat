using System.Linq;
using Editor.Reflect;
using Skill.Attribute;
using Skill.Skills;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Editor.ActionEditor
{
    [CustomPropertyDrawer(typeof(CombatIdSelectAttribute))]
    public class IdSelectDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            if (!EditorWindow.HasOpenInstances<SkillEditor.SkillEditor>())
            {
                return new PropertyField(property);
            }

            var idSelectAttribute = attribute as CombatIdSelectAttribute;
            var wnd = EditorWindow.GetWindow<SkillEditor.SkillEditor>();
            var choices = wnd.CombatConfig.GetFilteredDataList(idSelectAttribute.idType).ToList();
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