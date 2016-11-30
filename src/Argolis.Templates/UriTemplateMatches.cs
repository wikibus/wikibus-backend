using System;
using System.Collections.Generic;

namespace Argolis.Templates
{
    public class UriTemplateMatches
    {
        private readonly Dictionary<string, object> dictionary;

        public UriTemplateMatches(Dictionary<string, object> dictionary)
        {
            this.dictionary = dictionary;
        }

        public bool AreEmpty => this.dictionary.Count == 0;

        public TOut Get<TOut>(string key)
        {
            var valueType = Nullable.GetUnderlyingType(typeof(TOut));

            if (valueType == null)
            {
                valueType = typeof(TOut);
            }

            if (this.dictionary.ContainsKey(key))
            {
                return (TOut)Convert.ChangeType(this.dictionary[key], valueType);
            }

            return default(TOut);
        }
    }
}