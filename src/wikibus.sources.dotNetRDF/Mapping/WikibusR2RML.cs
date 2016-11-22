using System;
using System.Collections.Generic;
using Resourcer;
using TCode.r2rml4net;
using TCode.r2rml4net.Mapping;
using TCode.r2rml4net.Mapping.Fluent;
using TCode.r2rml4net.RDB;
using TCode.r2rml4net.Validation;
using Vocab;
using Wikibus.Common;

namespace Wikibus.Sources.DotNetRDF.Mapping
{
    /// <summary>
    /// R2RML mappings for the wikibus SQL database
    /// </summary>
    public class WikibusR2RML : IR2RML
    {
        private const string SourceGraphTemplate = "http://data.wikibus.org/graph/{SourceType}/{Id}/imported";
        private const string MagazineGraphTemplate = "http://data.wikibus.org/graph/magazine/{Id}/imported";
        private static readonly string SelectBookAuthorSql = Resource.AsString("SqlQueries.SelectBookAuthor.sql");
        private static readonly string SelectBrochureAndBook = Resource.AsString("SqlQueries.SelectBrochureAndBook.sql");
        private static readonly string SelectMagIssues = Resource.AsString("SqlQueries.SelectMagazineIssue.sql");
        private readonly Lazy<IR2RML> rml;
        private readonly IWikibusConfiguration config;
        private ITriplesMapConfiguration magazineMap;
        private ITriplesMapConfiguration magIssueMap;
        private ITriplesMapFromR2RMLViewConfiguration sourceMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="WikibusR2RML" /> class
        /// </summary>
        public WikibusR2RML(IWikibusConfiguration config)
        {
            this.config = config;
            this.rml = new Lazy<IR2RML>(this.CreateMappings);
        }

        /// <summary>
        /// Gets triple maps contained by this <see cref="T:TCode.r2rml4net.IR2RML" />
        /// </summary>
        public IEnumerable<ITriplesMap> TriplesMaps
        {
            get { return this.rml.Value.TriplesMaps; }
        }

        /// <summary>
        /// Gets or sets the <see cref="T:TCode.r2rml4net.RDB.ISqlQueryBuilder" />, which is used during generating triples
        /// </summary>
        public ISqlQueryBuilder SqlQueryBuilder
        {
            get { return this.rml.Value.SqlQueryBuilder; }
            set { this.rml.Value.SqlQueryBuilder = value; }
        }

        /// <summary>
        /// Gets or sets the SQL version validator implementation
        /// </summary>
        public ISqlVersionValidator SqlVersionValidator
        {
            get { return this.rml.Value.SqlVersionValidator; }
            set { this.rml.Value.SqlVersionValidator = value; }
        }

        private FluentR2RML CreateMappings()
        {
            var rml = new FluentR2RML();

            this.MapBooksAndBrochures(rml);
            this.MapMagazines(rml);
            this.MapMagazineIssues(rml);
            this.MapMetadata(rml);

            return rml;
        }

        private void MapMetadata(FluentR2RML rml)
        {
            var metadataMapping = rml.CreateTriplesMapFromR2RMLView("select Id from [Sources].[Magazine]");
            metadataMapping.SubjectMap.IsTemplateValued(MagazineGraphTemplate);

            var magazineMeta = metadataMapping.CreatePropertyObjectMap();
            magazineMeta.CreateRefObjectMap(this.magazineMap).AddJoinCondition("Id", "Id");
            magazineMeta.CreatePredicateMap().IsConstantValued(new Uri(Foaf.primaryTopic));

            metadataMapping = rml.CreateTriplesMapFromR2RMLView("select [Id], [SourceType] from [Sources].[Source]");
            metadataMapping.SubjectMap.IsTemplateValued(SourceGraphTemplate);

            var issueMeta = metadataMapping.CreatePropertyObjectMap();
            issueMeta.CreateRefObjectMap(this.magIssueMap).AddJoinCondition("Id", "Id");
            issueMeta.CreatePredicateMap().IsConstantValued(new Uri(Foaf.primaryTopic));

            var sourceMeta = metadataMapping.CreatePropertyObjectMap();
            sourceMeta.CreateRefObjectMap(this.sourceMap).AddJoinCondition("Id", "Id");
            sourceMeta.CreatePredicateMap().IsConstantValued(new Uri(Foaf.primaryTopic));
        }

        private void MapMagazines(FluentR2RML rml)
        {
            this.magazineMap = rml.CreateTriplesMapFromR2RMLView("select * from [Sources].[Magazine]");
            this.magazineMap.SubjectMap.IsTemplateValued(this.config.BaseResourceNamespace + "magazine/{Name}");
            this.magazineMap.SubjectMap.AddClass(new Uri(Schema.Periodical));
            this.magazineMap.SubjectMap.AddClass(new Uri(Wbo.Magazine));
            this.magazineMap.SubjectMap.CreateGraphMap().IsTemplateValued(MagazineGraphTemplate);

            this.MapMagazineTitle(this.magazineMap);
        }

        private void MapMagazineIssues(FluentR2RML rml)
        {
            var template = this.config.BaseResourceNamespace + "magazine/{Magazine}/issue/{MagIssueNumber}";

            this.magIssueMap = rml.CreateTriplesMapFromR2RMLView(SelectMagIssues);
            this.magIssueMap.SubjectMap.IsTemplateValued(template);
            this.magIssueMap.SubjectMap.AddClass(new Uri(Schema.PublicationIssue));
            this.magIssueMap.SubjectMap.CreateGraphMap().IsTemplateValued(SourceGraphTemplate);

            this.MapLanguages(this.magIssueMap);
            this.MapDate(this.magIssueMap);
            this.MapImage(this.magIssueMap);
            this.MapIssueParent(this.magIssueMap);
            this.MapPagesCount(this.magIssueMap);

            this.magIssueMap.MapColumn("MagIssueNumber", Schema.issueNumber, new Uri(Xsd.@string));
        }

        private void MapIssueParent(ITriplesMapConfiguration sourceMap)
        {
            var magazineMap = sourceMap.CreatePropertyObjectMap();
            magazineMap.CreatePredicateMap().IsConstantValued(new Uri(Schema.isPartOf));
            magazineMap.CreateRefObjectMap(this.magazineMap)
                .AddJoinCondition("MagIssueMagazine", "Id");
        }

        private void MapBooksAndBrochures(FluentR2RML rml)
        {
            this.sourceMap = rml.CreateTriplesMapFromR2RMLView(SelectBrochureAndBook);
            var template = this.config.BaseResourceNamespace + "{TypeLower}/{Id}";
            this.sourceMap.SubjectMap.IsTemplateValued(template);
            this.sourceMap.SubjectMap.CreateGraphMap().IsTemplateValued(SourceGraphTemplate);

            this.MapFolderName(this.sourceMap);
            this.MapType(this.sourceMap);
            this.MapLanguages(this.sourceMap);
            this.MapPagesCount(this.sourceMap);
            this.MapFolderCode(this.sourceMap);
            this.MapDate(this.sourceMap);
            this.MapBookAuthor(this.sourceMap);
            this.MapBookISBN(this.sourceMap);
            this.MapImage(this.sourceMap);
        }

        private void MapImage(ITriplesMapConfiguration sourceMap)
        {
            sourceMap.MapColumn("HasImage", Wbo.BaseUri + "hasImage", new Uri(Xsd.boolean));
        }

        private void MapBookISBN(ITriplesMapFromR2RMLViewConfiguration sourceMap)
        {
            sourceMap.MapColumn("BookISBN", Schema.isbn);
        }

        private void MapBookAuthor(ITriplesMapFromR2RMLViewConfiguration sourceMap)
        {
            var authorTriplesMap = sourceMap.R2RMLConfiguration.CreateTriplesMapFromR2RMLView(SelectBookAuthorSql);
            authorTriplesMap.SubjectMap.TermType.IsBlankNode().IsTemplateValued("author_{Id}");
            authorTriplesMap.SubjectMap.CreateGraphMap().IsTemplateValued(SourceGraphTemplate);

            authorTriplesMap.MapColumn("BookAuthor", Schema.name);

            var authorMap = sourceMap.CreatePropertyObjectMap();
            authorMap.CreatePredicateMap().IsConstantValued(new Uri(Schema.author));
            authorMap.CreateRefObjectMap(authorTriplesMap).AddJoinCondition("Id", "Id");
        }

        private void MapPagesCount(ITriplesMapConfiguration sourceMap)
        {
            sourceMap.MapColumn("Pages", Bibo.pages, new Uri(Xsd.integer));
        }

        private void MapFolderCode(ITriplesMapFromR2RMLViewConfiguration sourceMap)
        {
            sourceMap.MapColumn("FolderCode", DCTerms.identifier);
        }

        private void MapDate(ITriplesMapConfiguration sourceMap)
        {
            sourceMap
                .MapColumn("Year", Opus.year, new Uri(Xsd.gYear))
                .MapColumn("month", Opus.month, new Uri(Xsd.gMonth))
                .MapTemplate("{Year}-{Month}-{Day}", DCTerms.date, Xsd.date);
        }

        private void MapLanguages(ITriplesMapConfiguration sourceMap)
        {
            sourceMap
                .MapTemplate(Lexvo.iso639_1 + "{Language}", DCTerms.language)
                .MapTemplate(Lexvo.iso639_1 + "{Language2}", DCTerms.language);
        }

        private void MapType(ITriplesMapFromR2RMLViewConfiguration sourceMap)
        {
            sourceMap.MapTemplate(Wbo.BaseUri + "{Type}", Rdf.type);
        }

        private void MapFolderName(ITriplesMapFromR2RMLViewConfiguration sourceMap)
        {
            sourceMap.MapColumn("FolderName", DCTerms.title);
            sourceMap.MapColumn("BookTitle", DCTerms.title);
        }

        private void MapMagazineTitle(ITriplesMapConfiguration sourceMap)
        {
            sourceMap.MapColumn("Name", DCTerms.title);
        }
    }
}
