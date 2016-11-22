using JetBrains.Annotations;
using NullGuard;
using Vocab;

namespace Wikibus.Sources
{
    /// <summary>
    /// Represents a schema.org ImageObject
    /// </summary>
    [NullGuard(ValidationFlags.AllPublic ^ ValidationFlags.Properties)]
    public class Image
    {
        /// <summary>
        /// Gets or sets the content URL.
        /// </summary>
        public string ContentUrl { get; set; }

        /// <summary>
        /// Gets the thumbnail image.
        /// </summary>
        public Image Thumbnail
        {
            get
            {
                return new Image
                {
                    ContentUrl = this.ContentUrl + "/small"
                };
            }
        }

        [UsedImplicitly]
        private string Type
        {
            get { return Schema.ImageObject; }
        }

        /// <summary>
        /// Determines whether image should be serialized
        /// </summary>
        public bool ShouldSerializeThumbnail()
        {
            return this.ContentUrl.EndsWith("small") == false;
        }
    }
}
