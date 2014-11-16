using System;
using System.Collections.Generic;
using Nancy.RDF.Responses;
using Newtonsoft.Json.Linq;

namespace wikibus.sources.nancy
{
    /// <summary>
    /// Basic @context provider, which returns a fixed @context for a type
    /// </summary>
    public class SourcesContextProvider : IContextProvider
    {
        private readonly Dictionary<Type, JToken> _contexts = new Dictionary<Type, JToken>();

        /// <summary>
        /// Initializes a new instance of the <see cref="SourcesContextProvider"/> class.
        /// </summary>
        public SourcesContextProvider()
        {
            _contexts[typeof(Brochure)] = new JValue("http://wikibus.org/contexts/brochure.jsonld");
        }

        /// <summary>
        /// Gets the @context.
        /// </summary>
        /// <typeparam name="T">serialized type</typeparam>
        /// <exception cref="ContextNotFoundException{T}">when context not found for <typeparamref name="T"/></exception>
        public JToken GetContext<T>()
        {
            if (_contexts.ContainsKey(typeof(T)))
            {
                return _contexts[typeof(T)];
            }

            throw new ContextNotFoundException<T>();
        }
    }
}
