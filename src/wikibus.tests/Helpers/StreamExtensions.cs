using System.IO;
using Newtonsoft.Json.Linq;

namespace wikibus.tests.Helpers
{
    public static class StreamExtensions
    {
        public static dynamic ToJsonObject(this Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(stream))
            {
                return JObject.Parse(reader.ReadToEnd());
            }
        }
    }
}
