using System.Linq;
using VDS.RDF.Query.Optimisation;

/// <summary>
/// All code inside the Initialize method is ran as soon as the assembly is loaded.
/// </summary>
public static class ModuleInitializer
{
    /// <summary>
    /// Initializes the module.
    /// </summary>
    public static void Initialize()
    {
        SparqlOptimiser.RemoveOptimiser(SparqlOptimiser.AlgebraOptimisers.FirstOrDefault(o => o.GetType() == typeof(LazyBgpOptimiser)));
    }
}
