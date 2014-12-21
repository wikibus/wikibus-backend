using System;
using System.Collections.Generic;
using Resourcer;
using TCode.r2rml4net;
using TCode.r2rml4net.Mapping;
using TCode.r2rml4net.Mapping.Fluent;
using TCode.r2rml4net.RDB;
using TCode.r2rml4net.Validation;
using VDS.RDF.Parsing;

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
        private static readonly string SelectImages = Resource.AsString("SqlQueries.SelectImages.sql");
        private readonly Lazy<IR2RML> _rml;
        private ITriplesMapConfiguration _magazineMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="WikibusR2RML" /> class
        /// </summary>
        public WikibusR2RML()
        {
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
            _magazineMap.SubjectMap.IsTemplateValued("http://wikibus.org/magazine/{Name}");
            _magazineMap.SubjectMap.AddClass(new Uri("http://schema.org/Periodical"));
            _magazineMap.SubjectMap.AddClass(new Uri("http://wikibus.org/ontology#Magazine"));

            MapMagazineTitle(_magazineMap);
        }

        private void MapMagazineIssues(FluentR2RML rml)
        {
            const string template = "http://wikibus.org/magazine/{Magazine}/issue/{MagIssueNumber}";

            var magIssueMap = rml.CreateTriplesMapFromR2RMLView(SelectMagIssues);
            magIssueMap.SubjectMap.IsTemplateValued(template);

            MapLanguages(magIssueMap);
            MapDate(magIssueMap);
            MapImage(magIssueMap, template);
            MapIssueParent(magIssueMap);
            MapPagesCount(magIssueMap);

            magIssueMap.MapConstant(new Uri("http://schema.org/PublicationIssue"), new Uri(RdfSpecsHelper.RdfType))
                       .MapColumn(
                            "MagIssueNumber",
                            new Uri("http://schema.org/issueNumber"),
                            new Uri(XmlSpecsHelper.XmlSchemaDataTypeString));
        }

        private void MapIssueParent(ITriplesMapFromR2RMLViewConfiguration sourceMap)
        {
            var magazineMap = sourceMap.CreatePropertyObjectMap();
            magazineMap.CreatePredicateMap().IsConstantValued(new Uri("http://schema.org/isPartOf"));
            magazineMap.CreateRefObjectMap(_magazineMap)
                .AddJoinCondition("MagIssueMagazine", "Id");
        }

        private void MapBooksAndBrochures(FluentR2RML rml)
        {
            var sourceMap = rml.CreateTriplesMapFromR2RMLView(SelectBrochureAndBook);
            const string template = "http://wikibus.org/{TypeLower}/{Id}";
            sourceMap.SubjectMap.IsTemplateValued(template);

            MapFolderName(sourceMap);
            MapType(sourceMap);
            MapLanguages(sourceMap);
            MapPagesCount(sourceMap);
            MapFolderCode(sourceMap);
            MapDate(sourceMap);
            MapBookAuthor(sourceMap);
            MapBookISBN(sourceMap);
            MapImage(sourceMap, template);
        }

        private void MapImage(ITriplesMapFromR2RMLViewConfiguration sourceMap, string template)
        {
            var imageObjectMap = sourceMap.R2RMLConfiguration.CreateTriplesMapFromR2RMLView(SelectImages);
            imageObjectMap.SubjectMap.TermType.IsBlankNode().IsTemplateValued("image_{Id}");
            imageObjectMap.SubjectMap.AddClass(new Uri("http://schema.org/ImageObject"));

            imageObjectMap.MapTemplate(
                template + "/image",
                new Uri("http://schema.org/contentUrl"),
                new Uri("http://schema.org/URL"));

            var imageMap = sourceMap.CreatePropertyObjectMap();
            imageMap.CreatePredicateMap().IsConstantValued(new Uri("http://schema.org/image"));
            imageMap.CreateRefObjectMap(imageObjectMap).AddJoinCondition("Id", "Id");
        }

        private void MapBookISBN(ITriplesMapFromR2RMLViewConfiguration sourceMap)
        {
            sourceMap.MapColumn("BookISBN", new Uri("http://schema.org/isbn"));
        }

        private void MapBookAuthor(ITriplesMapFromR2RMLViewConfiguration sourceMap)
        {
            var authorTriplesMap = sourceMap.R2RMLConfiguration.CreateTriplesMapFromR2RMLView(SelectBookAuthorSql);
            authorTriplesMap.SubjectMap.TermType.IsBlankNode().IsTemplateValued("author_{Id}");

            authorTriplesMap.MapColumn("BookAuthor", new Uri("http://schema.org/name"));

            var authorMap = sourceMap.CreatePropertyObjectMap();
            authorMap.CreatePredicateMap().IsConstantValued(new Uri("http://schema.org/author"));
            authorMap.CreateRefObjectMap(authorTriplesMap).AddJoinCondition("Id", "Id");
        }

        private void MapPagesCount(ITriplesMapFromR2RMLViewConfiguration sourceMap)
        {
            sourceMap.MapColumn("Pages", new Uri("http://purl.org/ontology/bibo/pages"), new Uri(XmlSpecsHelper.XmlSchemaDataTypeInteger));
        }

        private void MapFolderCode(ITriplesMapFromR2RMLViewConfiguration sourceMap)
        {
            sourceMap.MapColumn("FolderCode", new Uri("http://purl.org/dc/terms/identifier"));
        }

        private void MapDate(ITriplesMapFromR2RMLViewConfiguration sourceMap)
        {
            sourceMap
                .MapColumn(
                    "Year",
                    new Uri("http://lsdis.cs.uga.edu/projects/semdis/opus#year"),
                    new Uri(XmlSpecsHelper.NamespaceXmlSchema + "gYear"))
                .MapColumn(
                    "month",
                    new Uri("http://lsdis.cs.uga.edu/projects/semdis/opus#month"),
                    new Uri(XmlSpecsHelper.NamespaceXmlSchema + "gMonth"))
                .MapTemplate(
                    "{Year}-{Month}-{Day}",
                    new Uri("http://purl.org/dc/terms/date"),
                    new Uri(XmlSpecsHelper.XmlSchemaDataTypeDate));
        }

        private void MapLanguages(ITriplesMapFromR2RMLViewConfiguration sourceMap)
        {
            sourceMap
                .MapTemplate("http://www.lexvo.org/page/iso639-1/{Language}", new Uri("http://purl.org/dc/terms/language"))
                .MapTemplate("http://www.lexvo.org/page/iso639-1/{Language2}", new Uri("http://purl.org/dc/terms/language"));
        }

        private void MapType(ITriplesMapFromR2RMLViewConfiguration sourceMap)
        {
            sourceMap.MapTemplate("http://wikibus.org/ontology#{Type}", new Uri(RdfSpecsHelper.RdfType));
        }

        private void MapFolderName(ITriplesMapFromR2RMLViewConfiguration sourceMap)
        {
            sourceMap.MapColumn("FolderName", new Uri("http://purl.org/dc/terms/title"));
            sourceMap.MapColumn("BookTitle", new Uri("http://purl.org/dc/terms/title"));
        }

        private void MapMagazineTitle(ITriplesMapConfiguration sourceMap)
        {
            sourceMap.MapColumn("Name", new Uri("http://purl.org/dc/terms/title"));
        }
    }
}
