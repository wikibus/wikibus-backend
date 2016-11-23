using System;
using System.Reflection;
using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Wikibus.Tests.sources.EF
{
    [Binding, Scope(Tag = "EF")]
    public class RetrievingSourceWithEfSteps
    {
        private static readonly MethodInfo CreateInstanceGeneric;
        private readonly EntitiFrameworkSourceTestContext context;

        static RetrievingSourceWithEfSteps()
        {
            CreateInstanceGeneric = typeof(TableHelperExtensionMethods).GetMethod(
                "CreateInstance",
                BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public,
                null,
                new[] {typeof(Table)},
                null);
        }

        public RetrievingSourceWithEfSteps(EntitiFrameworkSourceTestContext context)
        {
            this.context = context;
        }

        [Then(@"(.*) should match")]
        public void ThenResultShouldMatch(string typeName, Table table)
        {
            var createInstance = CreateInstanceGeneric.MakeGenericMethod(Type.GetType("Wikibus.Sources." + typeName + ", wikibus.sources"));

            var expected = createInstance.Invoke(null, new object[] { table });

            foreach (var row in table.Rows)
            {
                var property = context.Source.GetType().GetProperty(row[0]);
                var actulValue = property.GetValue(context.Source);
                var expectedValue = property.GetValue(expected);

                actulValue.Should().Be(expectedValue);
            }
        }

        [Then(@"Languages should contain (.*)")]
        public void ThenLanguagesShouldContain(string languageId)
        {
            context.Source.Languages.Should().Contain(language => language.Name == languageId);
        }

        [Then(@"Date should be (.*)")]
        public void ThenDateShouldBe(DateTime date)
        {
            context.Source.Date.Should().Be(date);
        }

        [Then(@"Id should be <(.*)>")]
        public void ThenIdShouldBe(string id)
        {
            context.Source.Id.Should().Be(new Uri(id));
        }

        [Then(@"Should have image (.*)")]
        public void ThenBrochureShouldHaveImage(string imgContentUrl)
        {
            context.Source.Image.Should().NotBeNull();
        }

        [Then(@"Book should be null")]
        [Then(@"Magazine Issue should be null")]
        [Then(@"Brochure should be null")]
        public void ThenBrochureShouldBeNull()
        {
            context.Source.Should().BeNull();
        }
    }
}