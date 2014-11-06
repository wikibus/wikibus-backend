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
        /// Gets the turtle type.
        /// </summary>
        public static RdfMediaType Turtle
        {
            get
            {
                return new RdfMediaType("text/turtle", "ttl");
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
