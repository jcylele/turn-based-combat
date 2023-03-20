using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Editor.SkillEditor
{
    public class ItemComparePanel : BaseItemPanel
    {
        private PropertyField mOriginPropertyField;
        private PropertyField mNewPropertyField;

        public ItemComparePanel(DataStore m_DataStore) : base(m_DataStore)
        {
        }

        protected override void OnBind()
        {
            mOriginPropertyField = root.Q<PropertyField>("origin");
            mNewPropertyField = root.Q<PropertyField>("new");
            InitButtons();
        }

        void InitButtons()
        {
            this.root.Q<Button>("btnSave")
                .RegisterCallback((ClickEvent evt) => { this.m_DataStore.OnCompareEnd(true); });
            this.root.Q<Button>("btnCancel").RegisterCallback((ClickEvent evt) =>
            {
                this.m_DataStore.OnCompareEnd(false);
            });
        }

        protected override void OnShow()
        {
            base.OnShow();
            if (this.m_DataStore.OriginEditingItemData != null)
            {
                mOriginPropertyField.BindProperty(this.m_DataStore.OriginEditingItemData.property);    
            }

            if (this.m_DataStore.OriginEditingItemData != null)
            {
                mNewPropertyField.BindProperty(this.m_DataStore.TopEditingItem.property);    
            }
        }
    }
}