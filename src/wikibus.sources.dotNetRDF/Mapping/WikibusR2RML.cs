using System;
using System.Collections.Generic;
using Resourcer;
using TCode.r2rml4net;
using TCode.r2rml4net.Mapping;
using TCode.r2rml4net.Mapping.Fluent;
using TCode.r2rml4net.RDB;
using TCode.r2rml4net.Validation;
using Vocab;
using wikibus.common;
using wikibus.common.Vocabularies;

namespace wikibus.sources.dotNetRDF.Mapping
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
        private readonly Lazy<IR2RML> _rml;
        private readonly IWikibusConfiguration _config;
        private ITriplesMapConfiguration _magazineMap;
        private ITriplesMapConfiguration _magIssueMap;
        private ITriplesMapFromR2RMLViewConfiguration _sourceMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="WikibusR2RML" /> class
        /// </summary>
        public WikibusR2RML(IWikibusConfiguration config)
        {
            _config = config;
            _rml = new Lazy<IR2RML>(CreateMappings);
        }

        /// <summary>
        /// Gets triple maps contained by this <see cref="T:TCode.r2rml4net.IR2RML" />
        /// </summary>
        public IEnumerable<ITriplesMap> TriplesMaps
        {
            get { return _rml.Value.TriplesMaps; }
        }

        /// <summary>
        /// Gets or sets the <see cref="T:TCode.r2rml4net.RDB.ISqlQueryBuilder" />, which is used during generating triples
        /// </summary>
        public ISqlQueryBuilder SqlQueryBuilder
        {
            get { return _rml.Value.SqlQueryBuilder; }
            set { _rml.Value.SqlQueryBuilder = value; }
        }

        /// <summary>
        /// Gets or sets the SQL version validator implementation
        /// </summary>
        public ISqlVersionValidator SqlVersionValidator
        {
            get { return _rml.Value.SqlVersionValidator; }
            set { _rml.Value.SqlVersionValidator = value; }
        }

        private FluentR2RML CreateMappings()
        {
            var rml = new FluentR2RML();

            MapBooksAndBrochures(rml);
            MapMagazines(rml);
            MapMagazineIssues(rml);
            MapMetadata(rml);

            return rml;
        }

        private void MapMetadata(FluentR2RML rml)
        {
            var metadataMapping = rml.CreateTriplesMapFromR2RMLView("select Id from [Sources].[Magazine]");
            metadataMapping.SubjectMap.IsTemplateValued(MagazineGraphTemplate);

            var magazineMeta = metadataMapping.CreatePropertyObjectMap();
            magazineMeta.CreateRefObjectMap(_magazineMap).AddJoinCondition("Id", "Id");
            magazineMeta.CreatePredicateMap().IsConstantValued(new Uri(Foaf.primaryTopic));

            metadataMapping = rml.CreateTriplesMapFromR2RMLView("select [Id], [SourceType] from [Sources].[Source]");
            metadataMapping.SubjectMap.IsTemplateValued(SourceGraphTemplate);

            var issueMeta = metadataMapping.CreatePropertyObjectMap();
            issueMeta.CreateRefObjectMap(_magIssueMap).AddJoinCondition("Id", "Id");
            issueMeta.CreatePredicateMap().IsConstantValued(new Uri(Foaf.primaryTopic));

            var sourceMeta = metadataMapping.CreatePropertyObjectMap();
            sourceMeta.CreateRefObjectMap(_sourceMap).AddJoinCondition("Id", "Id");
            sourceMeta.CreatePredicateMap().IsConstantValued(new Uri(Foaf.primaryTopic));
        }

        private void MapMagazines(FluentR2RML rml)
        {
            _magazineMap = rml.CreateTriplesMapFromR2RMLView("select * from [Sources].[Magazine]");
            _magazineMap.SubjectMap.IsTemplateValued(_config.BaseResourceNamespace + "magazine/{Name}");
            _magazineMap.SubjectMap.AddClass(new Uri(Schema.Periodical));
            _magazineMap.SubjectMap.AddClass(new Uri(Wbo.Magazine));
            _magazineMap.SubjectMap.CreateGraphMap().IsTemplateValued(MagazineGraphTemplate);

            MapMagazineTitle(_magazineMap);
        }

        private void MapMagazineIssues(FluentR2RML rml)
        {
            var template = _config.BaseResourceNamespace + "magazine/{Magazine}/issue/{MagIssueNumber}";

            _magIssueMap = rml.CreateTriplesMapFromR2RMLView(SelectMagIssues);
            _magIssueMap.SubjectMap.IsTemplateValued(template);
            _magIssueMap.SubjectMap.AddClass(new Uri(Schema.PublicationIssue));
            _magIssueMap.SubjectMap.CreateGraphMap().IsTemplateValued(SourceGraphTemplate);

            MapLanguages(_magIssueMap);
            MapDate(_magIssueMap);
            MapImage(_magIssueMap);
            MapIssueParent(_magIssueMap);
            MapPagesCount(_magIssueMap);

            _magIssueMap.MapColumn("MagIssueNumber", Schema.issueNumber, new Uri(Xsd.@string));
        }

        private void MapIssueParent(ITriplesMapConfiguration sourceMap)
        {
            var magazineMap = sourceMap.CreatePropertyObjectMap();
            magazineMap.CreatePredicateMap().IsConstantValued(new Uri(Schema.isPartOf));
            magazineMap.CreateRefObjectMap(_magazineMap)
                .AddJoinCondition("MagIssueMagazine", "Id");
        }

        private void MapBooksAndBrochures(FluentR2RML rml)
        {
            _sourceMap = rml.CreateTriplesMapFromR2RMLView(SelectBrochureAndBook);
            var template = _config.BaseResourceNamespace + "{TypeLower}/{Id}";
            _sourceMap.SubjectMap.IsTemplateValued(template);
            _sourceMap.SubjectMap.CreateGraphMap().IsTemplateValued(SourceGraphTemplate);

            MapFolderName(_sourceMap);
            MapType(_sourceMap);
            MapLanguages(_sourceMap);
            MapPagesCount(_sourceMap);
            MapFolderCode(_sourceMap);
            MapDate(_sourceMap);
            MapBookAuthor(_sourceMap);
            MapBookISBN(_sourceMap);
            MapImage(_sourceMap);
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
