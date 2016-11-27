namespace Wikibus.Sources.EF
{
    public class EntityWrapper<T>
    {
        public T Entity { get; set; }

        public bool HasImage { get; set; }
    }
}