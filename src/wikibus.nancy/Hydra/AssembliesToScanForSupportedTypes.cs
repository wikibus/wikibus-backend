using System.Collections.Generic;
using System.Reflection;
using Hydra.Discovery.SupportedClasses;
using wikibus.sources;

namespace wikibus.nancy.Hydra
{
    /// <summary>
    /// Sets up hydra supported classes source
    /// </summary>
    public class AssembliesToScanForSupportedTypes : AssemblyAnnotatedTypeSelector
    {
        /// <summary>
        /// Gets the assemblies to scan for supported classes
        /// </summary>
        protected override IEnumerable<Assembly> Assemblies
        {
            get
            {
                yield return typeof(EntryPoint).Assembly;
                yield return typeof(Source).Assembly;
            }
        }
    }
}