using Nancy;
using Nancy.Bootstrapper;
using Wikibus.Common;

namespace Wikibus.Nancy.Responses
{
    public class ContentLocationPipeline : IApplicationStartup
    {
        private readonly IWikibusConfiguration configuration;

        public ContentLocationPipeline(IWikibusConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void Initialize(IPipelines pipelines)
        {
            pipelines.AfterRequest += this.EnsureContentLocation;
        }

        private void EnsureContentLocation(NancyContext obj)
        {
            if (obj.Response.Headers.ContainsKey("Content-Location") == false)
            {
                obj.Response.Headers.Add("Content-Location", $"{this.configuration.BaseResourceNamespace.TrimEnd('/')}{obj.Request.Url.Path}{obj.Request.Url.Query}");
            }
        }
    }
}