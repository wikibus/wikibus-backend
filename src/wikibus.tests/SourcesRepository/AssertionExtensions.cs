using NUnit.Framework;

namespace wikibus.tests.SourcesRepository
{
    public static class AssertionExtensions
    {
        public static void AssertPropertyValue(this object model, string propName, object expected)
        {
            var value = ImpromptuInterface.Impromptu.InvokeGet(model, propName);

            Assert.That(value, Is.EqualTo(expected));
        }
    }
}
