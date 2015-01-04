using System;
using JsonLD.Entities.Context;
using Newtonsoft.Json.Linq;

namespace wikibus.common.JsonLd
{
    /// <summary>
    /// @context building helpers for prefixes
    /// </summary>
    public static class Prefix
    {
        /// <summary>
        /// Creates a prefix JProperty for vocabulary type
        /// </summary>
        public static JProperty Of(Type vocabulary)
        {
            var prefix = (string)vocabulary.GetField("Prefix").GetValue(null);
            var baseUri = (string)vocabulary.GetField("BaseUri").GetValue(null);

            return prefix.IsPrefixOf(baseUri);
        }
    }
}
