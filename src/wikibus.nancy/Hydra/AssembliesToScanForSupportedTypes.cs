using System.Collections.Generic;
using System.Reflection;
using Argolis.Hydra.Discovery.SupportedClasses;
using Wikibus.Sources;

namespace Wikibus.Nancy.Hydra
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