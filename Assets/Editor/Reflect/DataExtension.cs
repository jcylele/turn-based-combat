using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Skill.Data;
using Skill.Skills;

namespace Editor.Reflect
{
    public static class DataExtension
    {
        /// <summary>
        /// deep copy object, it's so convenient indeed
        /// <para>so far, just use it for <see cref="BaseEditItem"/> </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objSource">original object</param>
        /// <returns>copy of the original object</returns>
        public static T CopyObject<T>(this BaseEditItem objSource)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, objSource);
                stream.Position = 0;
                return (T)formatter.Deserialize(stream);
            }
        }

        public static bool IsSubOrSelf(this Type type1, Type type2)
        {
            return type1 == type2 || type1.IsSubclassOf(type2);
        }

        public static IEnumerable<BaseIdItem> GetFilteredDataList(this CombatConfig combatConfig, Type type)
        {
            return combatConfig.allData.Where(data => data.GetType().IsSubOrSelf(type));
        }

        public static int GetIdIndex<T>(this IList<T> list, int id) where T : BaseIdItem
        {
            for (var i = 0; i < list.Count; i++)
            {
                if (list[i].id == id)
                {
                    return i;
                }
            }

            return -1;
        }

        public static bool AddOrReplaceItem<T>(this IList<T> list, int originalID, T item, out string errString) where  T : BaseIdItem
        {
            errString = String.Empty;

            var originalIndex = list.GetIdIndex(originalID);
            if (originalID != item.id)
            {
                errString = $"id changed from {originalID} to {item.id}, notice for reference";
                
                var newIndex = list.GetIdIndex(item.id);
                if (newIndex != -1)
                {
                    errString = $"New ID {item.id} is duplicate, change it before saving";
                    return false;
                }
            }
            
            if (originalIndex == -1)
            {
                list.Add(item);
            }
            else
            {
                list[originalIndex] = item;
            }

            return true;
        }
        
        public static bool IsList(this Type type)
        {
            return type.IsGenericType 
                   && type.GetGenericTypeDefinition() == typeof(List<>);
        }
    }
}