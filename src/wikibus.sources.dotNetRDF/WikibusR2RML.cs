using System;
using System.Collections.Generic;
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
        private readonly IR2RML _rml;

        /// <summary>
        /// Initializes a new instance of the <see cref="WikibusR2RML"/> class.
        /// </summary>
        public WikibusR2RML()
        {
            var rml = new FluentR2RML();

            var brochureMap = rml.CreateTriplesMapFromR2RMLView(@"SELECT [Id]
      ,CASE [SourceType]
        WHEN 'folder' THEN 'Brochure'
        WHEN 'book' THEN 'Book'
        WHEN 'file' THEN 'File'
        WHEN 'magissue' THEN 'Issue'
       END as [Type]
      ,[Language]
      ,[Language2]
      ,[Pages]
      ,[Year]
      ,[Month]
      ,[Day]
      ,[Notes]
      ,[FolderCode]
      ,[FolderName]
      ,[BookTitle]
      ,[BookAuthor]
      ,[BookISBN]
      ,[MagIssueMagazine]
      ,[MagIssueNumber]
      ,[FileMimeType]
      ,[Url]
      ,[FileName]
  FROM [Sources].[Source]");
            brochureMap.SubjectMap.IsTemplateValued("http://wikibus.org/brochure/{Id}");

            var titleMap = brochureMap.CreatePropertyObjectMap();
            titleMap.CreatePredicateMap().IsConstantValued(new Uri("http://purl.org/dc/terms/title"));
            titleMap.CreateObjectMap().IsColumnValued("FolderName");

            var typeMap = brochureMap.CreatePropertyObjectMap();
            typeMap.CreatePredicateMap().IsConstantValued(new Uri(RdfSpecsHelper.RdfType));
            typeMap.CreateObjectMap().IsTemplateValued("http://wikibus.org/ontology#{Type}");

            var langMap = brochureMap.CreatePropertyObjectMap();
            langMap.CreatePredicateMap().IsConstantValued(new Uri("http://purl.org/dc/terms/language"));
            langMap.CreateObjectMap().IsTemplateValued("http://www.lexvo.org/page/iso639-1/{Language}");

            var lang1Map = brochureMap.CreatePropertyObjectMap();
            lang1Map.CreatePredicateMap().IsConstantValued(new Uri("http://purl.org/dc/terms/language"));
            lang1Map.CreateObjectMap().IsTemplateValued("http://www.lexvo.org/page/iso639-1/{Language2}");

            var pagesMap = brochureMap.CreatePropertyObjectMap();
            pagesMap.CreatePredicateMap().IsConstantValued(new Uri("http://purl.org/ontology/bibo/pages"));
            pagesMap.CreateObjectMap().IsColumnValued("Pages")
                                      .HasDataType(new Uri(XmlSpecsHelper.XmlSchemaDataTypeInteger));

            var codeMap = brochureMap.CreatePropertyObjectMap();
            codeMap.CreatePredicateMap().IsConstantValued(new Uri("http://purl.org/dc/terms/identifier"));
            codeMap.CreateObjectMap().IsColumnValued("FolderCode");

            var yearMap = brochureMap.CreatePropertyObjectMap();
            yearMap.CreatePredicateMap().IsConstantValued(new Uri("http://lsdis.cs.uga.edu/projects/semdis/opus#year"));
            yearMap.CreateObjectMap().IsColumnValued("Year")
                                     .HasDataType(new Uri(XmlSpecsHelper.NamespaceXmlSchema + "gYear"));

            var monthMap = brochureMap.CreatePropertyObjectMap();
            monthMap.CreatePredicateMap().IsConstantValued(new Uri("http://lsdis.cs.uga.edu/projects/semdis/opus#month"));
            monthMap.CreateObjectMap().IsColumnValued("Month")
                                      .HasDataType(new Uri(XmlSpecsHelper.NamespaceXmlSchema + "gMonth"));

            var dateMap = brochureMap.CreatePropertyObjectMap();
            dateMap.CreatePredicateMap().IsConstantValued(new Uri("http://purl.org/dc/terms/date"));
            ((ILiteralTermMapConfiguration)dateMap.CreateObjectMap().IsTemplateValued("{Year}-{Month}-{Day}")
                                     .IsLiteral())
                                     .HasDataType(new Uri(XmlSpecsHelper.XmlSchemaDataTypeDate));

            _rml = rml;
        }

        /// <summary>
        /// Gets triple maps contained by this <see cref="T:TCode.r2rml4net.IR2RML" />
        /// </summary>
        public IEnumerable<ITriplesMap> TriplesMaps
        {
            get { return _rml.TriplesMaps; }
        }

        /// <summary>
        /// Gets or sets the <see cref="T:TCode.r2rml4net.RDB.ISqlQueryBuilder" />, which is used during generating triples
        /// </summary>
        public ISqlQueryBuilder SqlQueryBuilder
        {
            get { return _rml.SqlQueryBuilder; }
            set { _rml.SqlQueryBuilder = value; }
        }

        /// <summary>
        /// Gets or sets the SQL version validator implementation
        /// </summary>
        public ISqlVersionValidator SqlVersionValidator
        {
            get { return _rml.SqlVersionValidator; }
            set { _rml.SqlVersionValidator = value; }
        }
    }
}
