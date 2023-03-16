using Skill.Reflect;
using UnityEngine.UIElements;

namespace Skill.Editor
{
    /// <summary>
    /// A simple class to maintain the life cycle of a panel
    /// </summary>
    public class BaseItemPanel
    {
        protected readonly DataStore m_DataStore;
        protected VisualElement root;

        public BaseItemPanel(DataStore m_DataStore)
        {
            this.m_DataStore = m_DataStore;
        }

        public void Bind(VisualElement root)
        {
            this.root = root;
            this.OnBind();

            if (this.Enabled)
            {
                this.OnShow();
            }
            RefreshVisibility();
        }

        protected virtual void OnBind()
        {

        }

        protected virtual void OnShow()
        {

        }

        private bool m_enabled = true;
        public bool Enabled
        {
            get
            {
                return this.m_enabled;
            }
            set
            {
                if (this.m_enabled == value) return;
                this.m_enabled = value;
                if (this.root == null) return;

                if (value)
                {
                    this.OnShow();
                }
                RefreshVisibility();
            }
        }

        private void RefreshVisibility()
        {
            if (this.root != null)
            {
                this.root.style.display = this.m_enabled ? DisplayStyle.Flex : DisplayStyle.None;
            }
        }
    }
}