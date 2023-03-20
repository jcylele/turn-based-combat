using System;
using System.Collections.Generic;
using System.Linq;
using Editor.Reflect;
using Skill.Data;
using Skill.Skills;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.SkillEditor
{
    /// <summary>
    /// maintains data of the editor
    /// <para>separates data and view</para>
    /// <para>refer to a data store plugin of vue framework</para>
    /// </summary>
    public class DataStore
    {
        public System.Action OnStackChanged;
        private readonly CombatConfig m_CombatConfig;
        private EditTypeMgr TypeMgr;

        private readonly List<EditingItemData> editStack;

        public EditingItemData OriginEditingItemData { get; private set; }

        private EditingCombatConfig mEditingCombatConfig;

        private IList<EditClassInfo> selectableClasses;
        private EditClassInfo selectedClass;
        private BaseIdItem selectedItem;
        public EItemPanel CurPanel { get; private set; }

        public DataStore(CombatConfig combatConfig)
        {
            this.m_CombatConfig = combatConfig;
            editStack = new List<EditingItemData>();
            TypeMgr = new EditTypeMgr(typeof(BaseEditItem));
            selectableClasses = TypeMgr.GetChildTypes(typeof(BaseIdItem), false, false, false);
            CurPanel = EItemPanel.Select;
        }

        public List<string> SelectableClassNames
        {
            get { return selectableClasses.Select(a => a.ShowName).ToList(); }
        }

        public List<string> EditableIDs
        {
            get
            {
                if (selectedClass == null)
                {
                    return new List<string>();
                }

                return this.m_CombatConfig.GetFilteredDataList(selectedClass.SelfType).Select(a => a.id.ToString())
                    .ToList();
            }
        }

        public void OnSelectTypeChanged(int index)
        {
            selectedClass = selectableClasses[index];
            selectedItem = null;
        }

        public void OnSelectIdChanged(int id)
        {
            selectedItem = this.m_CombatConfig.allData.FirstOrDefault(a => a.id == id);
        }

        public void StartEdit(bool isNew)
        {
            switch (isNew)
            {
                case true when selectedClass == null:
                    Debug.LogError("No Class Type Is Selected");
                    return;
                case false when selectedItem == null:
                    Debug.LogError("No Item Id Is Selected");
                    return;
            }

            mEditingCombatConfig = ScriptableObject.CreateInstance<EditingCombatConfig>();
            this.OriginEditingItemData = mEditingCombatConfig.NewEditingItemData(selectedClass, selectedItem, true);
            var data = mEditingCombatConfig.NewEditingItemData(selectedClass, selectedItem, false);
            this.PushItem(data);
        }

        public void PushItem(EditingItemData skillData)
        {
            this.editStack.Add(skillData);
            CurPanel = EItemPanel.Edit;
            OnStackChanged();
        }

        public void PopItem()
        {
            if (editStack.Count == 1)
            {
                CurPanel = EItemPanel.Compare;
            }
            else
            {
                editStack.RemoveAt(editStack.Count - 1);
            }

            OnStackChanged();
        }

        public void OnCompareEnd(bool bSave)
        {
            if (bSave)
            {
                var originalId = 0;
                if (OriginEditingItemData.Item != null)
                {
                    originalId = ((BaseIdItem)OriginEditingItemData.Item).id;
                }

                if (this.m_CombatConfig.allData.AddOrReplaceItem(
                        originalId,
                        TopEditingItem.Item as BaseIdItem,
                        out var errString))
                {
                    this.m_CombatConfig.allData.Sort();
                    EditorUtility.SetDirty(this.m_CombatConfig);
                    AssetDatabase.SaveAssets();
                }
                else
                {
                    Debug.Log(errString);
                    return;
                }
            }
            
            this.editStack.RemoveAt(editStack.Count - 1);
            this.OriginEditingItemData = null;
            this.mEditingCombatConfig = null;
            this.CurPanel = EItemPanel.Select;
            // AssetDatabase.CreateAsset(mEditingCombatConfig, "Assets/Resources/Temporary.asset");
            this.OnStackChanged();
        }

        private TreeViewItemData<EditClassInfo> NewTreeViewItem(EditClassInfo data)
        {
            List<TreeViewItemData<EditClassInfo>> children = null;
            if (data.Children.Count > 0)
            {
                children = new List<TreeViewItemData<EditClassInfo>>();
                foreach (var child in data.Children)
                {
                    children.Add(NewTreeViewItem(child.Value));
                }
            }

            return new TreeViewItemData<EditClassInfo>(data.SelfType.GetHashCode(), data, children);
        }

        public List<TreeViewItemData<EditClassInfo>> GenerateTreeView(Type baseType)
        {
            var baseTypeInfo = TypeMgr.GetTypeInfo(baseType);
            var baseTreeRoot = NewTreeViewItem(baseTypeInfo);
            return new List<TreeViewItemData<EditClassInfo>> { baseTreeRoot };
        }

        public EditingItemData TopEditingItem => this.editStack.Count > 0 ? this.editStack[^1] : null;
    }
}