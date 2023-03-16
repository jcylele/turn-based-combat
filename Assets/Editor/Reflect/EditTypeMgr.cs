using System;
using System.Collections.Generic;
using System.Reflection;
using Skill.Attribute;

namespace Skill.Reflect
{
    /// <summary>
    /// keeps reflection info of all types derived from one type
    /// </summary>
    public class EditTypeMgr
    {
        private Dictionary<Type, EditClassInfo> allClasses;

        public EditTypeMgr(Type rootType)
        {
            this.allClasses = CollectAllDerivedTypes(rootType);
        }

        private bool CheckType(EditClassInfo type, EditClassInfo baseType, bool bIncludeSelf, bool newable)
        {
            if (newable && type.IsAbstract) return false;
            if (type == baseType && !bIncludeSelf) return false;
            return true;
        }

        public IList<EditClassInfo> GetChildTypes(Type baseType, bool bRecusive, bool bIncludeSelf, bool newable)
        {
            IList<EditClassInfo> result = new List<EditClassInfo>();
            var baseTypeInfo = GetTypeInfo(baseType);
            if (bRecusive)
            {
                Queue<EditClassInfo> queue = new Queue<EditClassInfo>();
                queue.Enqueue(baseTypeInfo);

                while (queue.Count > 0)
                {
                    var parent = queue.Dequeue();
                    foreach (var pair in parent.Children)
                    {
                        queue.Enqueue(pair.Value);
                    }
                    if (CheckType(parent, baseTypeInfo, bIncludeSelf, newable))
                    {
                        result.Add(parent);
                    }
                }
            }
            else
            {
                if (CheckType(baseTypeInfo, baseTypeInfo, bIncludeSelf, newable))
                {
                    result.Add(baseTypeInfo);
                }
                foreach (var pair in baseTypeInfo.Children)
                {
                    if (CheckType(pair.Value, baseTypeInfo, bIncludeSelf, newable))
                    {
                        result.Add(pair.Value);
                    }
                }
            }
            return result;
        }

        public EditClassInfo GetTypeInfo(Type type)
        {
            if (allClasses.TryGetValue(type, out var skillClsInfo))
            {
                return skillClsInfo;
            }
            return null;
        }

        private Dictionary<Type, EditClassInfo> CollectAllDerivedTypes(Type rootType)
        {
            var typeMap = new Dictionary<Type, EditClassInfo>();
            Queue<EditClassInfo> queue = new Queue<EditClassInfo>();
            queue.Enqueue(new EditClassInfo(rootType));
            while (queue.Count > 0)
            {
                var parent = queue.Dequeue();
                typeMap.Add(parent.SelfType, parent);
                var attrList = parent.SelfType.GetCustomAttributes<CombatChildAttribute>();
                foreach (var attr in attrList)
                {
                    var child = new EditClassInfo(attr.childType);
                    parent.AddChild(child);
                    queue.Enqueue(child);
                }
            }
            return typeMap;
        }
    }
}