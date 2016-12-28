using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TunnelVisionLabs.Net;

namespace Wikibus.Sources.Nancy
{
    public class IriTemplate
    {
        public IriTemplate(UriTemplate template, IEnumerable<IriTemplateMapping> mappings)
        {
            this.Mappings = mappings.ToArray();
            this.Template = template.ToString();
        }

        [JsonProperty("mapping")]
        public IriTemplateMapping[] Mappings { get; set; }

        public string Template { get; set; }

        public virtual string Type => "IriTemplate";
    }
}