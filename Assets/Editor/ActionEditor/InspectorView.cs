using UnityEngine.UIElements;

namespace Editor.ActionEditor
{
    public class InspectorView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<InspectorView, UxmlTraits>
        {
        }

        UnityEditor.Editor editor;

        public InspectorView()
        {
        }

        public void UpdateSelection(UnityEngine.Object target)
        {
            Clear();
            UnityEngine.Object.DestroyImmediate(editor);
            editor = UnityEditor.Editor.CreateEditor(target);

            var container = new IMGUIContainer(() =>
            {
                if (editor.target)
                {
                    editor.OnInspectorGUI();
                }
            });
            Add(container);
        }
    }
}