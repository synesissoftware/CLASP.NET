
namespace Clasp.Internal
{
    using global::System.Collections.Generic;
    using global::System.Diagnostics;
    using global::System.Linq;

    internal static class CollectionUtil
    {
        public static List<T> ResizeList<T>(List<T> list, int size)
        {
            Debug.Assert(null != list);
            Debug.Assert(size >= 0);

            if (size < list.Count)
            {
                list.RemoveRange(size, list.Count - size);

                return list;
            }

            if (list.Count < size)
            {
                if (size > list.Capacity)
                {
                    list.Capacity = size;
                }

                list.AddRange(Enumerable.Repeat(default(T), size - list.Count));

                return list;
            }

            return list;
        }
    }
}
