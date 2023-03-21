using System.Collections.Generic;
using Skill.Base;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.SkillEditor
{
    public enum EItemPanel
    {
        Select,
        Edit,
        Compare,
    }
    
    /// <summary>
    /// Entrance of the skill editor
    /// </summary>
    public class SkillEditor : EditorWindow
    {
        [SerializeField] private VisualTreeAsset m_SelectViewAsset = default;

        [SerializeField] private VisualTreeAsset m_ItemViewAsset = default;
        [SerializeField] private VisualTreeAsset m_ItemCompareAsset = default;

        public CombatConfig CombatConfig { get; private set; }

        [MenuItem("Skill/Skill Editor")]
        public static void ShowExample()
        {
            SkillEditor wnd = GetWindow<SkillEditor>();
            wnd.titleContent = new GUIContent("SkillEditor");
        }

        public DataStore DataStore { get; private set; }
        private Dictionary<EItemPanel, BaseItemPanel> mItemPanels;

        public void CreateGUI()
        {
            this.CombatConfig = AssetDatabase.LoadAssetAtPath<CombatConfig>(EditorConst.ConfigPath_CombatConfig);
            this.mItemPanels = new Dictionary<EItemPanel, BaseItemPanel>();
            
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;

            // Instantiate UXML
            var selectView = m_SelectViewAsset.Instantiate();
            root.Add(selectView);

            // Instantiate UXML
            var editView = m_ItemViewAsset.Instantiate();
            root.Add(editView);
            
            var compareView = m_ItemCompareAsset.Instantiate();
            root.Add(compareView);

            DataStore = new DataStore(this.CombatConfig);
            DataStore.OnStackChanged = OnStackChanged;

            var selectPanel = new ItemSelectPanel(DataStore);
            selectPanel.Bind(selectView);
            this.mItemPanels.Add(EItemPanel.Select, selectPanel);

            var editPanel = new ItemEditPanel(DataStore);
            editPanel.Bind(editView);
            this.mItemPanels.Add(EItemPanel.Edit, editPanel);
            
            var comparePanel = new ItemComparePanel(DataStore);
            comparePanel.Bind(compareView);
            this.mItemPanels.Add(EItemPanel.Compare, comparePanel);

            OnStackChanged();
        }

        private void OnStackChanged()
        {
            foreach (var panel in mItemPanels)
            {
                panel.Value.Enabled = false;
            }

            this.mItemPanels[this.DataStore.CurPanel].Enabled = true;
        }
    }
}