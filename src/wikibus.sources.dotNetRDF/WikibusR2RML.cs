using System;
using System.Collections.Generic;
using Resourcer;
using TCode.r2rml4net;
using TCode.r2rml4net.Mapping;
using TCode.r2rml4net.Mapping.Fluent;
using TCode.r2rml4net.RDB;
using TCode.r2rml4net.Validation;
using wikibus.common;
using wikibus.common.Vocabularies;

namespace wikibus.sources.dotNetRDF
{
    /// <summary>
    /// R2RML mappings for the wikibus SQL database
    /// </summary>
    public class WikibusR2RML : IR2RML
    {
        private static readonly string SelectBookAuthorSql = Resource.AsString("SqlQueries.SelectBookAuthor.sql");
        private static readonly string SelectBrochureAndBook = Resource.AsString("SqlQueries.SelectBrochureAndBook.sql");
        private static readonly string SelectMagIssues = Resource.AsString("SqlQueries.SelectMagazineIssue.sql");
        private readonly Lazy<IR2RML> _rml;
        private readonly IWikibusConfiguration _config;
        private ITriplesMapConfiguration _magazineMap;

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

            return rml;
        }

        private void MapMagazines(FluentR2RML rml)
        {
            _magazineMap = rml.CreateTriplesMapFromR2RMLView("select * from [Sources].[Magazine]");
            _magazineMap.SubjectMap.IsTemplateValued(_config.BaseResourceNamespace + "magazine/{Name}");
            _magazineMap.SubjectMap.AddClass(new Uri(Schema.Periodical));
            _magazineMap.SubjectMap.AddClass(new Uri(Wbo.Magazine));

            MapMagazineTitle(_magazineMap);
        }

        private void MapMagazineIssues(FluentR2RML rml)
        {
            var template = _config.BaseResourceNamespace + "magazine/{Magazine}/issue/{MagIssueNumber}";

            var magIssueMap = rml.CreateTriplesMapFromR2RMLView(SelectMagIssues);
            magIssueMap.SubjectMap.IsTemplateValued(template);
            magIssueMap.SubjectMap.AddClass(new Uri(Schema.PublicationIssue));

            MapLanguages(magIssueMap);
            MapDate(magIssueMap);
            MapImage(magIssueMap);
            MapIssueParent(magIssueMap);
            MapPagesCount(magIssueMap);

            magIssueMap.MapColumn("MagIssueNumber", Schema.issueNumber, new Uri(Xsd.@string));
        }

        private void MapIssueParent(ITriplesMapFromR2RMLViewConfiguration sourceMap)
        {
            var magazineMap = sourceMap.CreatePropertyObjectMap();
            magazineMap.CreatePredicateMap().IsConstantValued(new Uri(Schema.isPartOf));
            magazineMap.CreateRefObjectMap(_magazineMap)
                .AddJoinCondition("MagIssueMagazine", "Id");
        }

        private void MapBooksAndBrochures(FluentR2RML rml)
        {
            var sourceMap = rml.CreateTriplesMapFromR2RMLView(SelectBrochureAndBook);
            var template = _config.BaseResourceNamespace + "{TypeLower}/{Id}";
            sourceMap.SubjectMap.IsTemplateValued(template);

            MapFolderName(sourceMap);
            MapType(sourceMap);
            MapLanguages(sourceMap);
            MapPagesCount(sourceMap);
            MapFolderCode(sourceMap);
            MapDate(sourceMap);
            MapBookAuthor(sourceMap);
            MapBookISBN(sourceMap);
            MapImage(sourceMap);
        }

        private void MapImage(ITriplesMapFromR2RMLViewConfiguration sourceMap)
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

            authorTriplesMap.MapColumn("BookAuthor", Schema.name);

            var authorMap = sourceMap.CreatePropertyObjectMap();
            authorMap.CreatePredicateMap().IsConstantValued(new Uri(Schema.author));
            authorMap.CreateRefObjectMap(authorTriplesMap).AddJoinCondition("Id", "Id");
        }

        private void MapPagesCount(ITriplesMapFromR2RMLViewConfiguration sourceMap)
        {
            sourceMap.MapColumn("Pages", Bibo.pages, new Uri(Xsd.integer));
        }

        private void MapFolderCode(ITriplesMapFromR2RMLViewConfiguration sourceMap)
        {
            sourceMap.MapColumn("FolderCode", DCTerms.identifier);
        }

        private void MapDate(ITriplesMapFromR2RMLViewConfiguration sourceMap)
        {
            sourceMap
                .MapColumn("Year", Opus.year, new Uri(Xsd.gYear))
                .MapColumn("month", Opus.month, new Uri(Xsd.gMonth))
                .MapTemplate("{Year}-{Month}-{Day}", DCTerms.date, Xsd.date);
        }

        private void MapLanguages(ITriplesMapFromR2RMLViewConfiguration sourceMap)
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
