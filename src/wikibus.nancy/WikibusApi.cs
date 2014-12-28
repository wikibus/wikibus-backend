using System;
using System.Collections.Generic;
using Hydra;
using JsonLD.Entities;
using wikibus.sources;

namespace wikibus.nancy
{
    /// <summary>
    /// The wikibus.org API Documentation
    /// </summary>
    [Class("http://www.w3.org/ns/hydra/core#ApiDocumentation")]
    internal class WikibusApi : ApiDocumentation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WikibusApi"/> class.
        /// </summary>
        public WikibusApi()
            : base(new Uri("http://data.wikibus.org/"))
        {
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public Uri Id
        {
            get
            {
                return new Uri("http://data.wikibus.org/doc");
            }
        }

        /// <summary>
        /// Gets the supported classes.
        /// </summary>
        public override IEnumerable<Class> SupportedClasses
        {
            get
            {
                return new[]
                {
                    typeof(EntryPoint).ToClass(),
                    typeof(Book).ToClass()
                };
            }
        }
    }
}
