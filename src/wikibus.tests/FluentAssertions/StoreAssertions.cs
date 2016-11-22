using System;
using FluentAssertions;
using FluentAssertions.Primitives;
using VDS.RDF;
using VDS.RDF.Query;
using VDS.RDF.Query.Builder;
using VDS.RDF.Query.Builder.Expressions;
using VDS.RDF.Query.Datasets;

namespace Wikibus.Tests.FluentAssertions
{
    public class StoreAssertions : ReferenceTypeAssertions<ITripleStore, StoreAssertions>
    {
        private readonly ISparqlQueryProcessor queryProcessor;

        public StoreAssertions(IInMemoryQueryableStore tripleStore)
        {
            queryProcessor = new LeviathanQueryProcessor(tripleStore);
        }

        public StoreAssertions(IGraph graph)
        {
            queryProcessor = new LeviathanQueryProcessor(new InMemoryDataset(graph));
        }

        protected override string Context
        {
            get { return "triple store"; }
        }

        public AndConstraint<StoreAssertions> MatchAsk(
            Action<ITriplePatternBuilder> getPtterns,
            Func<ExpressionBuilder, BooleanExpression> getFilters = null)
        {
            var queryBuilder = QueryBuilder.Ask().Where(getPtterns);

            if (getFilters != null)
            {
                queryBuilder.Filter(getFilters);
            }

            var query = queryBuilder.BuildQuery();
            var askResult = (SparqlResultSet)queryProcessor.ProcessQuery(queryBuilder.BuildQuery());
            askResult.Result.Should().BeTrue("Dataset contents should match query:{0}{1}", Environment.NewLine, query);

            return new AndConstraint<StoreAssertions>(this);
        }
    }
}
