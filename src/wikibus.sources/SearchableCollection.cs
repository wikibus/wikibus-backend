using Argolis.Hydra.Core;
using Argolis.Hydra.Resources;

namespace Wikibus.Sources
{
    public class SearchableCollection<T> : Collection<T>
    {
        public IriTemplate Search { get; set; }
    }
}