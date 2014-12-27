using System;
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
        public static void ReturnNotFoundWhenModelIsNull(this NancyModule module)
        {
            module.After += Ensure404When(model => model == null);
        }

        /// <summary>
        /// Ensures 404 status codes for null model or model matching given predicate.
        /// </summary>
        public static void ReturnNotFoundWhenModelIsNullOr(this NancyModule module, Func<dynamic, bool> modelIsInvalid)
        {
            module.After += Ensure404When(model => model == null || modelIsInvalid(model));
        }

        private static Action<NancyContext> Ensure404When(Func<object, bool> modelIsInvalid)
        {
            return context =>
            {
                var conneg = context.NegotiationContext;
                if (modelIsInvalid(conneg.DefaultModel) &&
                    conneg.MediaRangeModelMappings.Any() == false &&
                    IsSuccessStatusCode(context))
                {
                    context.Response.StatusCode = HttpStatusCode.NotFound;
                }
            };
        }

        private static bool IsSuccessStatusCode(NancyContext context)
        {
            return ((int)context.Response.StatusCode & 200) == 200;
        }
    }
}
