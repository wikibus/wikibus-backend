namespace wikibus.purl.nancy
{
    /// <summary>
    /// Represents an RDF media type
    /// </summary>
    public class RdfMediaType
    {
        private readonly string _mediaType;
        private readonly string _extension;

        private RdfMediaType(string mediaType, string extension)
        {
            _mediaType = mediaType;
            _extension = extension;
        }

        /// <summary>
        /// Gets the turtle mime type.
        /// </summary>
        public static RdfMediaType Turtle
        {
            get
            {
                return new RdfMediaType("text/turtle", "ttl");
            }
        }

        /// <summary>
        /// Gets the RDF/XML mime type.
        /// </summary>
        public static RdfMediaType RdfXml
        {
            get
            {
                return new RdfMediaType("application/rdf+xml", "rdf");
            }
        }

        /// <summary>
        /// Gets the JSON LD media type.
        /// </summary>
        public static RdfMediaType JsonLd
        {
            get
            {
                return new RdfMediaType("application/ld+json", "jsonld");
            }
        }

        /// <summary>
        /// Gets the n3 media type.
        /// </summary>
        public static RdfMediaType N3
        {
            get
            {
                return new RdfMediaType("text/rdf+n3", "n3");
            }
        }

        /// <summary>
        /// Gets the NTriples media type.
        /// </summary>
        public static RdfMediaType Ntriples
        {
            get
            {
                return new RdfMediaType("text/plain", "nt");
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
