using System.Collections.Generic;
using System.Data;

namespace Gnosis.Data.Hydrators
{
    public abstract class GenericHydrator<T> : Hydrator
    {
        public abstract void Hydrate(IDataReader dr, T data);
        public void Hydrate(IDictionary<string, string> input, T output)
        {
            foreach (KeyValuePair<string, string> kvp in input)
            {
                DoHydrate(kvp.Key, kvp.Value, output);
            }
        }

        protected abstract void DoHydrate(string key, string value, T output);
    }
}
