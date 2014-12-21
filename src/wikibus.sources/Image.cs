﻿using JsonLD.Entities;
using NullGuard;

namespace wikibus.sources
{
    /// <summary>
    /// Represents a schema.org ImageObject
    /// </summary>
    [Class("http://schema.org/ImageObject")]
    [NullGuard(ValidationFlags.ReturnValues)]
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
                    ContentUrl = ContentUrl + "/small"
                };
            }
        }

        /// <summary>
        /// Determines whether image should be serialized
        /// </summary>
        public bool ShouldSerializeThumbnail()
        {
            return ContentUrl.EndsWith("small") == false;
        }
    }
}
