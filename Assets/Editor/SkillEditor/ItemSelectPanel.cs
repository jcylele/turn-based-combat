using Skill.Reflect;
using UnityEngine.UIElements;

namespace Skill.Editor
{
    /// <summary>
    /// A panel to select or new instance for editing
    /// </summary>
    public class ItemSelectPanel : BaseItemPanel
    {
        private DropdownField ddType;
        private DropdownField ddID;

        public ItemSelectPanel(DataStore m_DataStore) : base(m_DataStore)
        {
        }

        void OnSelectTypeChanged(ChangeEvent<string> evt)
        {
            var target = evt.target as DropdownField;
            this.m_DataStore.OnSelectTypeChanged(target.index);
            ddID.choices = this.m_DataStore.EditableIDs;
            ddID.index = -1;
        }

        void OnSelectIdChanged(ChangeEvent<string> evt)
        {
            var id = 0;
            int.TryParse(evt.newValue, out id);
            this.m_DataStore.OnSelectIdChanged(id);
        }

        public void OnBtnNewClicked(ClickEvent evt)
        {
            this.m_DataStore.StartEdit(true);
        }

        public void OnBtnEditClicked(ClickEvent evt)
        {
            this.m_DataStore.StartEdit(false);
        }

        protected override void OnBind()
        {
            this.ddType = root.Q<DropdownField>("ddType");
            ddType.RegisterValueChangedCallback(OnSelectTypeChanged);

            this.ddID = root.Q<DropdownField>("ddID");
            ddID.RegisterValueChangedCallback(OnSelectIdChanged);

            var btnNew = root.Q<Button>("btnNew");
            btnNew.RegisterCallback<ClickEvent>(OnBtnNewClicked);

            var btnEdit = root.Q<Button>("btnEdit");
            btnEdit.RegisterCallback<ClickEvent>(OnBtnEditClicked);

            //will not change, so set once
            ddType.choices = m_DataStore.SelectableClassNames;
        }

        protected override void OnShow()
        {
            base.OnShow();

            ddID.choices = this.m_DataStore.EditableIDs;
            ddID.index = -1;
        }
    }
}