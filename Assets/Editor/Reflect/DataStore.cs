using Skill.Buffs;
using Skill.Data;
using Skill.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Skill.Reflect
{
    /// <summary>
    /// maintains data of the editor
    /// <para>separates data and view</para>
    /// <para>refer to a data store plugin of vue framewor</para>
    /// </summary>
    public class DataStore
    {
        public Action OnStackChanged;
        private readonly CombatConfig m_CombatConfig;
        private EditTypeMgr TypeMgr;

        private readonly List<EditingItemData> editStack;
        private IList<EditClassInfo> selectableClasses;
        private EditClassInfo selectedClass;
        private BaseIdItem selectedItem;

        public DataStore(CombatConfig combatConfig)
        {
            this.m_CombatConfig = combatConfig;
            editStack = new List<EditingItemData>();
            TypeMgr = new EditTypeMgr(typeof(BaseEditItem));
            selectableClasses = TypeMgr.GetChildTypes(typeof(BaseIdItem), false, false, false);
        }

        #region 数据接口
        public List<string> SelectableClassNames
        {
            get
            {
                return selectableClasses.Select(a => a.ShowName).ToList();
            }
        }

        public List<string> EditableIDs
        {
            get
            {
                if (selectedClass == null)
                {
                    return new List<string>();
                }
                var list = GetDataList(selectedClass.SelfType);
                return list.Select(a => a.id.ToString()).ToList();
            }
        }

        public bool IsEditing
        {
            get
            {
                return this.editStack.Count > 0;
            }
        }

        #endregion

        public void OnSelectTypeChanged(int index)
        {
            selectedClass = selectableClasses[index];
            selectedItem = null;
        }

        public void OnSelectIdChanged(int id)
        {
            var list = GetDataList(selectedClass.SelfType);
            selectedItem = list.FirstOrDefault(a => a.id == id);
        }

        public void StartEdit(bool isNew)
        {
            var data = new EditingIdItem()
            {
                baseType = selectedClass.SelfType,
            };
            if (isNew)
            {
                if (selectedClass == null)
                {
                    Debug.LogError("No Class Type Is Selected");
                    return;
                }
            }
            else
            {
                if (selectedItem == null)
                {
                    Debug.LogError("No Item Id Is Selected");
                    return;
                }
                data.originalId = selectedItem.id;
                data.Item = selectedItem;
            }

            this.PushItem(data);
        }

        public void PushItem(EditingItemData skillData)
        {
            if (skillData.Item != null)
            {
                //edit copy, not original object
                var copy = skillData.Item.CopyObject<BaseEditItem>();
                skillData.Item = copy;
            }
            this.editStack.Add(skillData);

            OnStackChanged();
        }

        public void PopItem(bool saveChange)
        {
            if (saveChange)
            {
                if (editStack.Count > 1)
                {
                    var val = editStack[editStack.Count - 1] as EditingFieldData;
                    var obj = editStack[editStack.Count - 2];

                    val.fieldInfo.SetValue(obj.Item, val.Item);
                }
                else
                {
                    if (!SaveData(editStack[0] as EditingIdItem))
                    {
                        return;
                    }
                    EditorUtility.SetDirty(this.m_CombatConfig);
                    AssetDatabase.SaveAssets();
                }
            }

            editStack.RemoveAt(editStack.Count - 1);
            OnStackChanged();
        }

        private int IndexOf<T>(IList<T> list, int id) where T : BaseIdItem
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].id == id)
                {
                    return i;
                }
            }
            return -1;
        }

        private bool AddOrReplaceItem<T>(IList<T> list, int id, T item) where T : BaseIdItem
        {
            var index = IndexOf(list, id);
            if (index == -1)
            {
                index = IndexOf(list, item.id);
                if (index != -1)
                {
                    Debug.LogError($"{item.id} is duplicate, change it before saving");
                    return false;
                }
                list.Add(item);
            }
            else
            {
                if (id != item.id)
                {
                    Debug.LogWarning($"id changed from {id} to {item.id}, notice for reference");
                }
                list[index] = item;
            }

            return true;
        }

        public bool SaveData(EditingIdItem data)
        {
            switch (data.Item)
            {
                case BaseSkill skill:
                    return AddOrReplaceItem(m_CombatConfig.skills, data.originalId, skill);
                case BaseBuff buff:
                    return AddOrReplaceItem(m_CombatConfig.buffs, data.originalId, buff);
                default:
                    throw new Exception($"Invalid EditingIdItem {data}");
            }
        }

        public IEnumerable<BaseIdItem> GetDataList(Type type)
        {
            if (type.IsSubOrSelf(typeof(BaseSkill)))
            {
                return m_CombatConfig.skills;
            }
            else if (type.IsSubOrSelf(typeof(BaseBuff)))
            {
                return m_CombatConfig.buffs;
            }
            else
            {
                throw new Exception($"Invalid Type: {type}");
            }
        }

        #region Item Editing
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

        public EditingItemData TopEditingItem
        {
            get
            {
                if (this.editStack.Count > 0)
                {
                    return this.editStack[this.editStack.Count - 1];
                }
                return null;
            }
        }

        public List<EditFieldInfo> GetFieldList(Type type)
        {
            var classInfo = TypeMgr.GetTypeInfo(type);
            return classInfo.FieldInfoList;
        }

        public List<string> GetIdList(Type type)
        {
            var list = this.GetDataList(type);
            return list.Where(x => x.GetType().IsSubOrSelf(type)).Select(x => x.id.ToString()).ToList();
        }

        #endregion
    }
}