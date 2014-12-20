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

            var typeMap = _magazineMap.CreatePropertyObjectMap();
            typeMap.CreatePredicateMap().IsConstantValued(new Uri(RdfSpecsHelper.RdfType));
            typeMap.CreateObjectMap().IsConstantValued(new Uri("http://schema.org/Periodical"));

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

            var typeMap = magIssueMap.CreatePropertyObjectMap();
            typeMap.CreatePredicateMap().IsConstantValued(new Uri(RdfSpecsHelper.RdfType));
            typeMap.CreateObjectMap().IsConstantValued(new Uri("http://schema.org/PublicationIssue"));

            var numberMap = magIssueMap.CreatePropertyObjectMap();
            numberMap.CreatePredicateMap().IsConstantValued(new Uri("http://schema.org/issueNumber"));
            numberMap.CreateObjectMap().IsColumnValued("MagIssueNumber").HasDataType(XmlSpecsHelper.XmlSchemaDataTypeString);
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
            sourceMap.SubjectMap.IsTemplateValued("http://wikibus.org/{TypeLower}/{Id}");

            MapFolderName(sourceMap);
            MapType(sourceMap);
            MapLanguages(sourceMap);
            MapPagesCount(sourceMap);
            MapFolderCode(sourceMap);
            MapDate(sourceMap);
            MapBookAuthor(sourceMap);
            MapBookISBN(sourceMap);
        }

        private void MapImage(ITriplesMapFromR2RMLViewConfiguration sourceMap, string template)
        {
            var imageMap = sourceMap.CreatePropertyObjectMap();
            imageMap.CreatePredicateMap().IsConstantValued(new Uri("http://schema.org/image"));
            imageMap.CreateObjectMap().IsTemplateValued(template + "/{HasImage}");
        }

        private void MapBookISBN(ITriplesMapFromR2RMLViewConfiguration sourceMap)
        {
            var isbnMap = sourceMap.CreatePropertyObjectMap();
            isbnMap.CreatePredicateMap().IsConstantValued(new Uri("http://schema.org/isbn"));
            isbnMap.CreateObjectMap().IsColumnValued("BookISBN");
        }

        private void MapBookAuthor(ITriplesMapFromR2RMLViewConfiguration sourceMap)
        {
            var authorTriplesMap = sourceMap.R2RMLConfiguration.CreateTriplesMapFromR2RMLView(SelectBookAuthorSql);
            authorTriplesMap.SubjectMap.TermType.IsBlankNode()
                .IsTemplateValued("author_{Id}");

            var authorName = authorTriplesMap.CreatePropertyObjectMap();
            authorName.CreatePredicateMap().IsConstantValued(new Uri("http://schema.org/name"));
            authorName.CreateObjectMap().IsColumnValued("BookAuthor");

            var authorMap = sourceMap.CreatePropertyObjectMap();
            authorMap.CreatePredicateMap().IsConstantValued(new Uri("http://schema.org/author"));
            authorMap.CreateRefObjectMap(authorTriplesMap).AddJoinCondition("Id", "Id");
        }

        private void MapPagesCount(ITriplesMapFromR2RMLViewConfiguration sourceMap)
        {
            var pagesMap = sourceMap.CreatePropertyObjectMap();
            pagesMap.CreatePredicateMap().IsConstantValued(new Uri("http://purl.org/ontology/bibo/pages"));
            pagesMap.CreateObjectMap().IsColumnValued("Pages")
                .HasDataType(new Uri(XmlSpecsHelper.XmlSchemaDataTypeInteger));
        }

        private void MapFolderCode(ITriplesMapFromR2RMLViewConfiguration sourceMap)
        {
            var codeMap = sourceMap.CreatePropertyObjectMap();
            codeMap.CreatePredicateMap().IsConstantValued(new Uri("http://purl.org/dc/terms/identifier"));
            codeMap.CreateObjectMap().IsColumnValued("FolderCode");
        }

        private void MapDate(ITriplesMapFromR2RMLViewConfiguration sourceMap)
        {
            var yearMap = sourceMap.CreatePropertyObjectMap();
            yearMap.CreatePredicateMap().IsConstantValued(new Uri("http://lsdis.cs.uga.edu/projects/semdis/opus#year"));
            yearMap.CreateObjectMap().IsColumnValued("Year")
                .HasDataType(new Uri(XmlSpecsHelper.NamespaceXmlSchema + "gYear"));

            var monthMap = sourceMap.CreatePropertyObjectMap();
            monthMap.CreatePredicateMap().IsConstantValued(new Uri("http://lsdis.cs.uga.edu/projects/semdis/opus#month"));
            monthMap.CreateObjectMap().IsColumnValued("Month")
                .HasDataType(new Uri(XmlSpecsHelper.NamespaceXmlSchema + "gMonth"));

            var dateMap = sourceMap.CreatePropertyObjectMap();
            dateMap.CreatePredicateMap().IsConstantValued(new Uri("http://purl.org/dc/terms/date"));
            ((ILiteralTermMapConfiguration)dateMap.CreateObjectMap().IsTemplateValued("{Year}-{Month}-{Day}")
                .IsLiteral())
                .HasDataType(new Uri(XmlSpecsHelper.XmlSchemaDataTypeDate));
        }

        private void MapLanguages(ITriplesMapFromR2RMLViewConfiguration sourceMap)
        {
            var langMap = sourceMap.CreatePropertyObjectMap();
            langMap.CreatePredicateMap().IsConstantValued(new Uri("http://purl.org/dc/terms/language"));
            langMap.CreateObjectMap().IsTemplateValued("http://www.lexvo.org/page/iso639-1/{Language}");

            var lang1Map = sourceMap.CreatePropertyObjectMap();
            lang1Map.CreatePredicateMap().IsConstantValued(new Uri("http://purl.org/dc/terms/language"));
            lang1Map.CreateObjectMap().IsTemplateValued("http://www.lexvo.org/page/iso639-1/{Language2}");
        }

        private void MapType(ITriplesMapFromR2RMLViewConfiguration sourceMap)
        {
            var typeMap = sourceMap.CreatePropertyObjectMap();
            typeMap.CreatePredicateMap().IsConstantValued(new Uri(RdfSpecsHelper.RdfType));
            typeMap.CreateObjectMap().IsTemplateValued("http://wikibus.org/ontology#{Type}");
        }

        private void MapFolderName(ITriplesMapFromR2RMLViewConfiguration sourceMap)
        {
            var titleMap = sourceMap.CreatePropertyObjectMap();
            titleMap.CreatePredicateMap().IsConstantValued(new Uri("http://purl.org/dc/terms/title"));
            titleMap.CreateObjectMap().IsColumnValued("FolderName");
            titleMap.CreateObjectMap().IsColumnValued("BookTitle");
        }

        private void MapMagazineTitle(ITriplesMapConfiguration sourceMap)
        {
            var titleMap = sourceMap.CreatePropertyObjectMap();
            titleMap.CreatePredicateMap().IsConstantValued(new Uri("http://purl.org/dc/terms/title"));
            titleMap.CreateObjectMap().IsColumnValued("Name");
        }
    }
}
