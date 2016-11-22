using NUnit.Framework;

namespace Wikibus.Tests.sources.dotNetRDF.SourcesRepository
{
    public static class AssertionExtensions
    {
        public static void AssertPropertyValue(this object model, string propName, object expected)
        {
            var value = ImpromptuInterface.Impromptu.InvokeGetChain(model, propName);

            Assert.That(value, Is.EqualTo(expected));
        }
    }
}
