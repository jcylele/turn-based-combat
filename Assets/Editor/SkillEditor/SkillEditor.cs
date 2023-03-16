using Skill.Data;
using Skill.Reflect;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Skill.Editor
{
    /// <summary>
    /// Entrance of the skill editor
    /// </summary>
    public class SkillEditor : EditorWindow
    {
        [SerializeField]
        private VisualTreeAsset m_SelectViewAsset = default;

        [SerializeField]
        private VisualTreeAsset m_ItemViewAsset = default;

        private CombatConfig m_CombatConfig = default;

        [MenuItem("Skill/Skill Editor")]
        public static void ShowExample()
        {
            SkillEditor wnd = GetWindow<SkillEditor>();
            wnd.titleContent = new GUIContent("SkillEditor");
        }

        private DataStore m_DataStore;
        private ItemSelectPanel selectPanel;
        private ItemEditPanel editPanel;

        public void CreateGUI()
        {
            this.m_CombatConfig = AssetDatabase.LoadAssetAtPath<CombatConfig>("Assets/Resources/CombatConfig.asset");

            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;

            // Instantiate UXML
            var selectView = m_SelectViewAsset.Instantiate();
            root.Add(selectView);

            // Instantiate UXML
            var editView = m_ItemViewAsset.Instantiate();
            root.Add(editView);

            m_DataStore = new DataStore(this.m_CombatConfig);
            m_DataStore.OnStackChanged = OnStackChanged;

            this.selectPanel = new ItemSelectPanel(m_DataStore);
            this.selectPanel.Bind(selectView);

            this.editPanel = new ItemEditPanel(m_DataStore);
            this.editPanel.Bind(editView);

            OnStackChanged();
        }

        private void OnStackChanged()
        {
            this.selectPanel.Enabled = false;
            this.editPanel.Enabled = false;

            if (this.m_DataStore.IsEditing)
            {
                this.editPanel.Enabled = true;
            }
            else
            {
                this.selectPanel.Enabled = true;
            }
        }
    }
}