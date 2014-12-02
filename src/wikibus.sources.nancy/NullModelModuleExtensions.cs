using System.Linq;
using Nancy;

namespace wikibus.sources.nancy
{
    /// <summary>
    /// Ensures 404 status when model is null
    /// </summary>
    public static class NullModelModuleExtensions
    {
        /// <summary>
        /// Ensures 404 status codes for null model.
        /// </summary>
        public static void EnsureNotFoundStatusCodes(this NancyModule module)
        {
            module.After += EnsureNotFoundStatus;
        }

        private static void EnsureNotFoundStatus(NancyContext context)
        {
            var conneg = context.NegotiationContext;
            if (conneg.DefaultModel == null &&
                conneg.MediaRangeModelMappings.Any() == false)
            {
                context.Response.StatusCode = HttpStatusCode.NotFound;
            }
        }
    }
}
