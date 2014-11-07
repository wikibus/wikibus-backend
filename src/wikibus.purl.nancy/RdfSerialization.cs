namespace wikibus.purl.nancy
{
    /// <summary>
    /// Represents an RDF media type
    /// </summary>
    public class RdfSerialization
    {
        private readonly string _mediaType;
        private readonly string _extension;

        private RdfSerialization(string mediaType, string extension)
        {
            _mediaType = mediaType;
            _extension = extension;
        }

        /// <summary>
        /// Gets the turtle mime type.
        /// </summary>
        public static RdfSerialization Turtle
        {
            get
            {
                return new RdfSerialization("text/turtle", "ttl");
            }
        }

        /// <summary>
        /// Gets the RDF/XML mime type.
        /// </summary>
        public static RdfSerialization RdfXml
        {
            get
            {
                return new RdfSerialization("application/rdf+xml", "rdf");
            }
        }

        /// <summary>
        /// Gets the JSON LD media type.
        /// </summary>
        public static RdfSerialization JsonLd
        {
            get
            {
                return new RdfSerialization("application/ld+json", "jsonld");
            }
        }

        /// <summary>
        /// Gets the n3 media type.
        /// </summary>
        public static RdfSerialization N3
        {
            get
            {
                return new RdfSerialization("text/rdf+n3", "n3");
            }
        }

        /// <summary>
        /// Gets the NTriples media type.
        /// </summary>
        public static RdfSerialization Ntriples
        {
            get
            {
                return new RdfSerialization("text/plain", "nt");
            }
        }

        /// <summary>
        /// Gets the type of the media.
        /// </summary>
        public string MediaType
        {
            get { return _mediaType; }
        }

        /// <summary>
        /// Gets the extension.
        /// </summary>
        public string Extension
        {
            get { return _extension; }
        }
    }
}
