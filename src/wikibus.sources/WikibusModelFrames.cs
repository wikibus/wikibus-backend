using Argolis.Hydra.Resources;
using JsonLD.Entities;
using Newtonsoft.Json.Linq;
using Resource = Resourcer.Resource;

namespace Wikibus.Sources
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
            this.SetFrame(typeof(Book), JObject.Parse("{ '@type': 'wbo:Book' }"));
            this.SetFrame(typeof(Brochure), JObject.Parse("{ '@type': 'wbo:Brochure' }"));
            this.SetFrame(typeof(Issue), JObject.Parse("{ '@type': 'schema:PublicationIssue' }"));
            this.SetFrame(typeof(Collection<Book>), CollectionFrame);
            this.SetFrame(typeof(Collection<Brochure>), CollectionFrame);
            this.SetFrame(typeof(Collection<Magazine>), CollectionFrame);
            this.SetFrame(typeof(Collection<Issue>), CollectionFrame);
        }
    }
}
