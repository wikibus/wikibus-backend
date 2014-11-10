using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using JsonLD.Core;
using Nancy;
using Nancy.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using VDS.RDF;
using VDS.RDF.Parsing.Handlers;
using VDS.RDF.Writing.Formatting;

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
            if (string.IsNullOrWhiteSpace(contentType))
            {
                return false;
            }

            return contentType.Equals("text/turtle", StringComparison.InvariantCultureIgnoreCase);
        }

        /// <inheritdoc />
        public void Serialize<TModel>(string contentType, TModel model, Stream outputStream)
        {
            using (var writer = new StreamWriter(new UnclosableStreamWrapper(outputStream)))
            {
                var json = JsonConvert.SerializeObject(
                    model,
                    Formatting.Indented,
                    new JsonSerializerSettings
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                        });

                JObject jsObject = JObject.Parse(json);
                jsObject["@context"] = JObject.FromObject(new
                    {
                        title = "http://purl.org/dc/terms/title"
                    });

                var rdf = (RDFDataset)JsonLdProcessor.ToRDF(jsObject);

                VDS.RDF.IRdfHandler h = new WriteThroughHandler(new TurtleFormatter(), writer);
                h.StartRdf();

                foreach (var triple in rdf.GetQuads("@default"))
                {
                    h.HandleTriple(new Triple(
                                       CreateNode(h, triple.GetSubject()),
                                       CreateNode(h, triple.GetPredicate()),
                                       CreateNode(h, triple.GetObject())));
                }

                h.EndRdf(true);
            }
        }

        private INode CreateNode(IRdfHandler rdfHandler, RDFDataset.Node node)
        {
            if (node.IsIRI())
            {
                return rdfHandler.CreateUriNode(new Uri(node.GetValue()));
            }

            if (node.IsBlankNode())
            {
                return rdfHandler.CreateBlankNode(node.GetValue());
            }

            return rdfHandler.CreateLiteralNode(node.GetValue(), new Uri(node.GetDatatype()));
        }
    }
}
