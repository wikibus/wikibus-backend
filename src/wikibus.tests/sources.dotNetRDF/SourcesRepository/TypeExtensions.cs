using System;

namespace wikibus.tests.SourcesRepository
{
    internal static class TypeExtensions
    {
        internal static object GetDefaultValue(this Type t)
        {
            if (t.IsValueType && Nullable.GetUnderlyingType(t) == null)
            {
                return Activator.CreateInstance(t);
            }

            return null;
        }
    }
}
