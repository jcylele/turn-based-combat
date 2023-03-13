using System;
using System.Collections.Generic;
using System.Reflection;

namespace Skill.Reflect
{
    public class EditClassInfo
    {
        private readonly Type self;
        private List<EditFieldInfo> fieldInfoList;
        private readonly Dictionary<Type, EditClassInfo> mChildren;

        public Dictionary<Type, EditClassInfo> Children => mChildren;

        public List<EditFieldInfo> FieldInfoList
        {
            get
            {
                if (fieldInfoList == null)
                {
                    InitFields();
                }
                return fieldInfoList;
            }
        }

        public EditClassInfo(Type self)
        {
            this.self = self;
            this.mChildren = new Dictionary<Type, EditClassInfo>();
        }

        public string ShowName
        {
            get
            {
                return self.Name;
            }
        }

        public void InitFields()
        {
            var fieldList = self.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            fieldInfoList = new List<EditFieldInfo>(fieldList.Length);
            foreach (var fieldInfo in fieldList)
            {
                fieldInfoList.Add(new EditFieldInfo(fieldInfo));
            }
        }

        public object NewInstance()
        {
            return Activator.CreateInstance(self);
        }

        public bool IsAbstract => self.IsAbstract;

        public Type SelfType => self;

        public void AddChild(EditClassInfo child)
        {
            this.mChildren.Add(child.SelfType, child);
        }
    }
}
