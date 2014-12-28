using System;
using NullGuard;

namespace wikibus.nancy.Hydra
{
    /// <summary>
    /// Marks a property available for invoking GET
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    [NullGuard(ValidationFlags.ReturnValues)]
    public class AllowGetAttribute : Attribute
    {
    }
}
