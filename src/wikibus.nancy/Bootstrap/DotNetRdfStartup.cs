using System.Linq;
using Nancy.Bootstrapper;
using VDS.RDF.Query.Optimisation;

namespace Wikibus.Nancy
{
    /// <summary>
    /// Performs data back end setup
    /// </summary>
    public class DotNetRdfStartup : IApplicationStartup
    {
        /// <summary>
        /// Perform any initialization tasks
        /// </summary>
        public void Initialize(IPipelines pipelines)
        {
            SparqlOptimiser.RemoveOptimiser(SparqlOptimiser.AlgebraOptimisers.FirstOrDefault(o => o.GetType() == typeof(LazyBgpOptimiser)));
        }
    }
}
