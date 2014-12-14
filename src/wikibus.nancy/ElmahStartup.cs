using Nancy;
using Nancy.Bootstrapper;
using Nancy.Elmah;

namespace wikibus.nancy
{
    /// <summary>
    /// Wires logging framework with the application
    /// </summary>
    public class ElmahStartup : IApplicationStartup
    {
        /// <summary>
        /// Perform logging framework initialization task
        /// </summary>
        public void Initialize(IPipelines pipelines)
        {
            Elmahlogging.Enable(pipelines, "elmah", new string[0], new[] { HttpStatusCode.NotFound, HttpStatusCode.InsufficientStorage, });
        }
    }
}
