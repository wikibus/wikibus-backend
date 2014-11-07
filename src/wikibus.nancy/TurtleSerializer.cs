using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using JsonLD.Core;
using Nancy;

namespace wikibus.nancy
{
    /// <summary>
    /// Serializer for RDF data types
    /// </summary>
    public class TurtleSerializer : ISerializer
    {
        /// <inheritdoc />
        public IEnumerable<string> Extensions { get; private set; }

        /// <inheritdoc />
        public bool CanSerialize(string contentType)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void Serialize<TModel>(string contentType, TModel model, Stream outputStream)
        {
            throw new NotImplementedException();
        }
    }
}
