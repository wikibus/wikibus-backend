using System;
using System.Reflection;
using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using wikibus.sources;

namespace wikibus.tests.sources.EF
{
    [Binding, Scope(Tag = "EF")]
    public class RetrievingSourceWithEFSteps
    {
        private static readonly MethodInfo CreateInstanceGeneric = Info.OfMethod("TechTalk.SpecFlow", "TechTalk.SpecFlow.Assist.TableHelperExtensionMethods", "CreateInstance", "Table");
        private readonly EntitiFrameworkSourceTestContext _context;

        public RetrievingSourceWithEFSteps(EntitiFrameworkSourceTestContext context)
        {
            _context = context;
        }

        [Then(@"(.*) should match")]
        public void ThenResultShouldMatch(string typeName, Table table)
        {
            var createInstance = CreateInstanceGeneric.MakeGenericMethod(Type.GetType("wikibus.sources." + typeName + ", wikibus.sources"));

            var expected = createInstance.Invoke(null, new object[] { table });

            foreach (var row in table.Rows)
            {
                var property = _context.Source.GetType().GetProperty(row[0]);
                var actulValue = property.GetValue(_context.Source);
                var expectedValue = property.GetValue(expected);

                actulValue.Should().Be(expectedValue);
            }
        }

        [Then(@"Languages should contain (.*)")]
        public void ThenLanguagesShouldContain(string languageId)
        {
            _context.Source.Languages.Should().Contain(language => language.Name == languageId);
        }

        [Then(@"Date should be (.*)")]
        public void ThenDateShouldBe(DateTime date)
        {
            _context.Source.Date.Should().Be(date);
        }

        [Then(@"Id should be <(.*)>")]
        public void ThenIdShouldBe(string id)
        {
            _context.Source.Id.Should().Be(new Uri(id));
        }

        [Then(@"Should have image (.*)")]
        public void ThenBrochureShouldHaveImage(string imgContentUrl)
        {
            _context.Source.Image.Should().NotBeNull();
        }

        [Then(@"Book should be null")]
        [Then(@"Magazine Issue should be null")]
        [Then(@"Brochure should be null")]
        public void ThenBrochureShouldBeNull()
        {
            _context.Source.Should().BeNull();
        }
    }
}