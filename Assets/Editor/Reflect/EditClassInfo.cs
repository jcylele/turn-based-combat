using Skill.Attribute;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Skill.Reflect
{
    /// <summary>
    /// reflection info of a class type
    /// </summary>
    public class EditClassInfo
    {
        private readonly Type classType;
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

        public EditClassInfo(Type clsType)
        {
            this.classType = clsType;
            this.mChildren = new Dictionary<Type, EditClassInfo>();
        }

        public string ShowName
        {
            get
            {
                var showAttr = classType.GetCustomAttribute<CombatShowAttribute>();
                if (showAttr != null)
                {
                    return showAttr.showName;
                }
                return classType.Name;
            }
        }

        public void InitFields()
        {
            var fieldList = classType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            fieldInfoList = new List<EditFieldInfo>(fieldList.Length);
            foreach (var fieldInfo in fieldList)
            {
                fieldInfoList.Add(new EditFieldInfo(fieldInfo));
            }
        }

        public object NewInstance()
        {
            return Activator.CreateInstance(classType);
        }

        public bool IsAbstract => classType.IsAbstract;

        public Type SelfType => classType;

        public void AddChild(EditClassInfo child)
        {
            this.mChildren.Add(child.SelfType, child);
        }
    }
}
