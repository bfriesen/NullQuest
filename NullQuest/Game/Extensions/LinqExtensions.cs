using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace NullQuest.Game.Extensions
{
    public static class LinqExtensions
    {
        public static bool HaveSameItems<T>(this IEnumerable<T> self, IEnumerable<T> other)
        {
            var list1Groups = self.ToLookup(i => i);
            var list2Groups = other.ToLookup(i => i);
            return list1Groups.Count == list2Groups.Count
               && list1Groups.All(g => g.Count() == list2Groups[g.Key].Count());
        }

        public static T DeepClone<T>(this T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;
                return (T)formatter.Deserialize(ms);
            }
        }
    }
}
