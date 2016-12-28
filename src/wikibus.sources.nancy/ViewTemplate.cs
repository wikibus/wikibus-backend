using System.Collections.Generic;
using Hydra.Resources;
using TunnelVisionLabs.Net;

namespace Wikibus.Sources.Nancy
{
    public class ViewTemplate : IriTemplate, IView
    {
        public ViewTemplate(UriTemplate template, IEnumerable<IriTemplateMapping> mappings)
            : base(template, mappings)
        {
        }

        public override string Type => "ViewTemplate";
    }
}