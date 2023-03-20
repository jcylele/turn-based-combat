using System;
using System.Collections.Generic;
using System.Reflection;
using Skill.Attribute;

namespace Editor.Reflect
{
    /// <summary>
    /// reflection info of a class type
    /// </summary>
    public class EditClassInfo
    {
        public Dictionary<Type, EditClassInfo> Children { get; set; }
        public EditClassInfo(Type clsType)
        {
            this.SelfType = clsType;
            this.Children = new Dictionary<Type, EditClassInfo>();
        }

        public string ShowName
        {
            get
            {
                var showAttr = SelfType.GetCustomAttribute<CombatShowAttribute>();
                if (showAttr != null)
                {
                    return showAttr.showName;
                }
                return SelfType.Name;
            }
        }

        public object NewInstance()
        {
            return Activator.CreateInstance(SelfType);
        }

        public bool IsAbstract => SelfType.IsAbstract;

        public Type SelfType { get; }

        public void AddChild(EditClassInfo child)
        {
            this.Children.Add(child.SelfType, child);
        }
    }
}
