using System;
using System.Collections.Generic;
using Nancy.RDF;
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
            _contexts[typeof(Source)] = new JValue("http://wikibus.org/contexts/source.jsonld");
        }

        public JToken GetContext(Type modelType)
        {
            if (_contexts.ContainsKey(modelType))
            {
                return _contexts[modelType];
            }

            throw new ContextNotFoundException<object>();
        }

        public JObject GetExpandedContext(Type modelType)
        {
            throw new NotImplementedException();
        }
    }
}
