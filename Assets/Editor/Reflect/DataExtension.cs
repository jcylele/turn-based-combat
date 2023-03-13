using Skill.Skills;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Skill.Reflect
{
    public static class DataExtension
    {
        /// <summary>
        /// deep copy object, it's so convenient indeed
        /// <para>so far, just use it for <see cref="BaseEditItem"/> </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objSource">original object</param>
        /// <returns>copy</returns>
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
    }
}
