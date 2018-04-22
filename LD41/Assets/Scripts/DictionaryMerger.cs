using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class DictionaryMerger<T> 
    {
        public static Dictionary<T, int> merge( Dictionary<T, int> d1, Dictionary<T, int> d2)
        {
            Dictionary<T, int> result = new Dictionary<T, int>();

            foreach (KeyValuePair<T, int> entry in d1)
                result.Add(entry.Key, entry.Value);
            foreach (KeyValuePair<T, int> entry in d2)
                result[entry.Key] += entry.Value;

            return result;
        }
    }

}
