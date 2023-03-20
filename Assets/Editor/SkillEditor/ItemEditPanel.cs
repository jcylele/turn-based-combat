using System.Collections.Generic;
using Editor.Reflect;
using Skill.Skills;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.SkillEditor
{
    /// <summary>
    /// A panel which edits class instances or fields
    /// </summary>
    public class ItemEditPanel : BaseItemPanel
    {
        private EditingItemData editingItemData;
        private TreeView typeTreeView;
        private PropertyField propertyField;

        public ItemEditPanel(DataStore m_DataStore) : base(m_DataStore)
        {
        }

        protected override void OnBind()
        {
            typeTreeView = root.Q<TreeView>();
            propertyField = root.Q<PropertyField>();
            InitTreeView();
            root.Q<Button>("btnBack").clicked += () =>
            {
                this.m_DataStore.PopItem();
            };
        }

        private void InitTreeView()
        {
            // Set TreeView.makeItem to initialize each node in the tree.
            typeTreeView.makeItem = () => new Label();

            // Set TreeView.bindItem to bind an initialized node to a data item.
            typeTreeView.bindItem = FillTreeItem;

            typeTreeView.autoExpand = true;
            typeTreeView.selectionChanged += (IEnumerable<object> items) =>
            {
                //one item only actually
                foreach (var item in items)
                {
                    this.OnTypeChanged(item as EditClassInfo);
                }
            };
        }

        private void FillTreeItem(VisualElement element, int index)
        {
            var data = typeTreeView.GetItemDataForIndex<EditClassInfo>(index);
            (element as Label).text = data.ShowName;
            element.SetEnabled(!data.IsAbstract);
        }

        protected override void OnShow()
        {
            base.OnShow();

            this.editingItemData = this.m_DataStore.TopEditingItem;
            if (this.editingItemData == null) return;

            var itemData = this.m_DataStore.GenerateTreeView(editingItemData.BaseType);
            typeTreeView.SetRootItems(itemData);
            typeTreeView.Rebuild();

            //use hashcode as the key
            typeTreeView.SetSelectionById(editingItemData.CurType.GetHashCode());
        }

        private void OnTypeChanged(EditClassInfo classInfo)
        {
            if (classInfo == null || classInfo.IsAbstract)
            {
                RefreshPropertyField();
                return;
            }

            if (editingItemData.Item == null || editingItemData.Item.GetType() != classInfo.SelfType)
            {
                editingItemData.Item = classInfo.NewInstance() as BaseEditItem;
            }

            Debug.Log(editingItemData.Item);
            RefreshPropertyField();
        }

        private void RefreshPropertyField()
        {
            //PropertyField is great!
            // propertyField.Unbind();
            propertyField.BindProperty(this.editingItemData.property);
            // propertyField.label = this.editingItemData.CurType.Name;
            // this.root.Q<Button>("btnSave").RegisterCallback((ClickEvent evt) => { this.m_DataStore.PopItem(); });
        }
    }
}