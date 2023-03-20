using Skill.Skills;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Editor.SkillEditor
{
    /// <summary>
    /// Custom drawer for nested classes,
    /// only effective in SkillEditor
    /// <para>most times this method is only triggered once</para>
    /// </summary>
    [CustomPropertyDrawer(typeof(BaseNoIdItem), true)]
    public class BaseNoIdItemEditingDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var asset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(EditorConst.UxmlPath_NoIdItem);

            var root = asset.Instantiate();

            var propertyField = root.Q<PropertyField>();
            propertyField.BindProperty(property);

            bool canEdit = false;
            bool showEditButton = false;
            var btnEdit = root.Q<Button>();

            if (property.serializedObject.targetObject.GetType() != typeof(EditingCombatConfig))
            {
                canEdit = false;
                showEditButton = false;
            }
            else
            {
                var wnd = EditorWindow.GetWindow<SkillEditor>();
                canEdit = property.propertyPath == wnd.DataStore.TopEditingItem.property.propertyPath;
                showEditButton = !canEdit;

                if (showEditButton)
                {
                    btnEdit.clicked += () =>
                    {
                        var newItem = wnd.DataStore.TopEditingItem.Forward(property);
                        wnd.DataStore.PushItem(newItem);
                    };
                }
            }

            propertyField.SetEnabled(canEdit);
            btnEdit.style.display = showEditButton
                ? DisplayStyle.Flex
                : DisplayStyle.None;

            return root;
        }
    }
}