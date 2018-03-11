using JsonLD.Entities.Context;
using Newtonsoft.Json.Linq;

namespace Wikibus.Sources
{
    public class Hex
    {
        public static JToken Context => new JObject("hex".IsPrefixOf("http://hydra-ex.rest/vocab/"));
    }
}