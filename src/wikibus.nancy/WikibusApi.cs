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
    [Class(Hydra.Hydra.ApiDocumentation)]
    internal class WikibusApi : ApiDocumentation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WikibusApi"/> class.
        /// </summary>
        public WikibusApi(Uri entryPoint)
            : base(entryPoint)
        {
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public string Id
        {
            get { return new Uri(Entrypoint, "doc").ToString(); }
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
                    typeof(Book).ToClass(),
                    typeof(Brochure).ToClass(),
                    typeof(Magazine).ToClass(),
                    typeof(Issue).ToClass()
                };
            }
        }
    }
}
