using Hydra.Resources;
using JsonLD.Entities;
using Newtonsoft.Json.Linq;
using Resourcer;

namespace wikibus.sources
{
    /// <summary>
    /// Provider JSON-LD frames for wikibus models
    /// </summary>
    /// <seealso cref="JsonLD.Entities.StaticFrameProvider" />
    public class WikibusModelFrames : StaticFrameProvider
    {
        private static readonly JObject CollectionFrame = JObject.Parse(Resource.AsString("Frames.Collection.jsonld"));

        /// <summary>
        /// Initializes a new instance of the <see cref="WikibusModelFrames"/> class.
        /// </summary>
        public WikibusModelFrames()
        {
            SetFrame(typeof(Book), JObject.Parse("{ '@type': 'wbo:Book' }"));
            SetFrame(typeof(Brochure), JObject.Parse("{ '@type': 'wbo:Brochure' }"));
            SetFrame(typeof(Issue), JObject.Parse("{ '@type': 'schema:PublicationIssue' }"));
            SetFrame(typeof(Collection<Book>), CollectionFrame);
            SetFrame(typeof(Collection<Brochure>), CollectionFrame);
            SetFrame(typeof(Collection<Magazine>), CollectionFrame);
            SetFrame(typeof(Collection<Issue>), CollectionFrame);
        }
    }
}
