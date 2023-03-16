using Skill.Attribute;
using Skill.Reflect;
using Skill.Skills;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Skill.Editor
{
    /// <summary>
    /// A panel which edits class instances or fields
    /// </summary>
    public class ItemEditPanel : BaseItemPanel
    {
        private EditingItemData editingItemData;
        private TreeView typeTreeView;
        private ListView fieldList;

        public ItemEditPanel(DataStore m_DataStore) : base(m_DataStore)
        {
        }

        protected override void OnBind()
        {
            typeTreeView = root.Q<TreeView>();
            fieldList = root.Q<ListView>();
            InitTreeView();
            InitListView();
            InitButtons();
        }

        void InitButtons()
        {
            this.root.Q<Button>("btnSave").RegisterCallback((ClickEvent evt) =>
            {
                this.m_DataStore.PopItem(true);
            });
            this.root.Q<Button>("btnCancel").RegisterCallback((ClickEvent evt) =>
            {
                this.m_DataStore.PopItem(false);
            });
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
                //one item only
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

        private void InitListView()
        {
            // Set ListView.makeItem to initialize each entry in the list.
            fieldList.makeItem = MakeField;

            // Set ListView.bindItem to bind an initialized entry to a data item.
            fieldList.bindItem = (VisualElement element, int index) =>
            {
                BindField(element, fieldList.itemsSource[index] as EditFieldInfo);
            };
        }

        protected override void OnShow()
        {
            base.OnShow();

            this.editingItemData = this.m_DataStore.TopEditingItem;
            if (this.editingItemData != null)
            {
                var itemData = this.m_DataStore.GenerateTreeView(editingItemData.BaseType);
                typeTreeView.SetRootItems(itemData);
                typeTreeView.Rebuild();

                var curType = editingItemData.Item == null
                    ? editingItemData.BaseType
                    : editingItemData.Item.GetType();
                typeTreeView.SetSelectionById(curType.GetHashCode());
            }
        }

        public void OnTypeChanged(EditClassInfo classInfo)
        {
            if (classInfo == null || classInfo.IsAbstract)
            {
                RefreshFieldList();
                return;
            }
            if (editingItemData.Item == null || editingItemData.Item.GetType() != classInfo.SelfType)
            {
                editingItemData.Item = classInfo.NewInstance() as BaseEditItem;
            }
            Debug.Log(editingItemData.Item);
            RefreshFieldList();
        }

        public void RefreshFieldList()
        {
            if (editingItemData.Item == null)
            {
                fieldList.itemsSource = new List<EditFieldInfo>();
            }
            else
            {
                fieldList.itemsSource = m_DataStore.GetFieldList(editingItemData.Item.GetType());
            }
            fieldList.Rebuild();
        }

        private static VisualTreeAsset fieldAsset;

        private static Dictionary<Type, string> fieldElemNames = new Dictionary<Type, string>()
    {
        {typeof(int), "Int"},
        {typeof(string), "String"},
        {typeof(bool), "Bool"},
        {typeof(Enum), "Enum"},
        {typeof(BaseNoIdItem), "Class"},
        {typeof(CombatIdSelectAttribute), "ID" },
    };

        public static VisualTreeAsset FieldAsset
        {
            get
            {
                if (fieldAsset == null)
                {
                    fieldAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/SkillEditor/ItemField.uxml");
                }
                return fieldAsset;
            }
        }

        public static VisualElement MakeField()
        {
            var root = FieldAsset.Instantiate();

            foreach (var fieldName in fieldElemNames.Values)
            {
                root.Q(fieldName).style.display = DisplayStyle.None;
            }

            return root;
        }

        void BindSimpleField<T>(BaseField<T> baseField, EditFieldInfo editFieldInfo)
        {
            baseField.label = editFieldInfo.ShowName;
            baseField.value = editFieldInfo.GetValue<T>(editingItemData.Item);
            baseField.RegisterValueChangedCallback((ChangeEvent<T> evt) =>
            {
                editFieldInfo.SetValue(editingItemData.Item, evt.newValue);
            });
        }

        void BindField(VisualElement element, EditFieldInfo editFieldInfo)
        {
            VisualElement visibleElement = null;
            if (editFieldInfo.FieldType == typeof(int))
            {
                if (editFieldInfo.IDSelectType != null)
                {
                    var field = element.Q<DropdownField>(fieldElemNames[typeof(CombatIdSelectAttribute)]);
                    field.label = editFieldInfo.ShowName;

                    field.choices = this.m_DataStore.GetIdList(editFieldInfo.IDSelectType);
                    var val = editFieldInfo.GetValue<int>(editingItemData.Item);
                    field.value = val.ToString();
                    field.RegisterCallback((ChangeEvent<string> evt) =>
                    {
                        if (int.TryParse(evt.newValue, out var id))
                        {
                            editFieldInfo.SetValue(editingItemData.Item, id);
                        }
                    });

                    visibleElement = field;
                }
                else
                {
                    var field = element.Q<IntegerField>(fieldElemNames[typeof(int)]);
                    BindSimpleField(field, editFieldInfo);

                    visibleElement = field;
                }
            }
            else if (editFieldInfo.FieldType == typeof(bool))
            {
                var field = element.Q<Toggle>(fieldElemNames[typeof(bool)]);
                BindSimpleField(field, editFieldInfo);

                visibleElement = field;
            }
            else if (editFieldInfo.FieldType == typeof(string))
            {
                var field = element.Q<TextField>(fieldElemNames[typeof(string)]);
                BindSimpleField(field, editFieldInfo);

                visibleElement = field;
            }
            else if (editFieldInfo.FieldType.IsEnum)
            {
                var field = element.Q<EnumField>(fieldElemNames[typeof(Enum)]);
                field.label = editFieldInfo.ShowName;
                var val = editFieldInfo.GetValue<Enum>(editingItemData.Item);
                field.Init(val);
                field.value = val;
                field.RegisterValueChangedCallback((ChangeEvent<Enum> evt) =>
                {
                    editFieldInfo.SetValue(editingItemData.Item, evt.newValue);
                });

                visibleElement = field;
            }
            else if (editFieldInfo.FieldType.IsSubclassOf(typeof(BaseNoIdItem)))
            {
                var field = element.Q(fieldElemNames[typeof(BaseNoIdItem)]);

                field.Q<Label>().text = editFieldInfo.ShowName;
                var btn = field.Q<Button>();
                btn.text = "Edit";
                btn.RegisterCallback((ClickEvent evt) =>
                {
                    EditingFieldData data = new EditingFieldData
                    {
                        Item = editFieldInfo.GetValue<BaseEditItem>(editingItemData.Item),
                        fieldInfo = editFieldInfo,
                    };
                    this.m_DataStore.PushItem(data);
                });

                visibleElement = field;
            }
            else
            {
                throw new Exception($"Unexpected Field Type: {editFieldInfo.FieldType}");
            }

            visibleElement.style.display = DisplayStyle.Flex;
        }
    }
}