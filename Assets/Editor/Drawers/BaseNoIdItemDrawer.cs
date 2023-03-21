using Editor.Reflect;
using Editor.SkillEditor;
using Skill.Base;
using Skill.Skills;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Editor.Drawers
{
    /// <summary>
    /// Custom drawer for nested classes,
    /// only effective in SkillEditor
    /// <para>most times this method is only triggered once</para>
    /// </summary>
    [CustomPropertyDrawer(typeof(BaseNoIdItem), true)]
    public class BaseNoIdItemDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var asset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(EditorConst.UxmlPath_NoIdItem);

            var root = asset.Instantiate();

            var propertyField = root.Q<PropertyField>();
            propertyField.BindProperty(property);

            bool canEdit = true;
            bool showEditButton = false;
            var btnEdit = root.Q<Button>();

            if (property.serializedObject.targetObject.GetType() == typeof(EditingCombatConfig))
            {
                var wnd = EditorWindow.GetWindow<SkillEditor.SkillEditor>();
                canEdit = property.propertyPath == wnd.DataStore.TopEditingItem.Property.propertyPath;
                showEditButton = !canEdit;

                if (showEditButton)
                {
                    btnEdit.clicked += () =>
                    {
                        var newItem = new EditingItemData(property);
                        wnd.DataStore.PushItem(newItem);
                    };
                }
            }

            propertyField.SetEnabled(canEdit);
            if (property.managedReferenceValue != null)
            {
                propertyField.label = property.managedReferenceValue.ToString();
            }
            btnEdit.style.display = showEditButton
                ? DisplayStyle.Flex
                : DisplayStyle.None;

            return root;
        }
    }
}