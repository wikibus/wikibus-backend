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
        private readonly Lazy<IR2RML> _rml = new Lazy<IR2RML>(CreateMappings);

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

        private static FluentR2RML CreateMappings()
        {
            var rml = new FluentR2RML();

            var sourceMap = rml.CreateTriplesMapFromR2RMLView(Resource.AsString("SqlQueries.SelectSources.sql"));
            sourceMap.SubjectMap.IsTemplateValued("http://wikibus.org/brochure/{Id}");

            MapFolderName(sourceMap);
            MapType(sourceMap);
            MapLanguages(sourceMap);
            MapPagesCount(sourceMap);
            MapFolderCode(sourceMap);
            MapDate(sourceMap);

            return rml;
        }

        private static void MapPagesCount(ITriplesMapFromR2RMLViewConfiguration sourceMap)
        {
            var pagesMap = sourceMap.CreatePropertyObjectMap();
            pagesMap.CreatePredicateMap().IsConstantValued(new Uri("http://purl.org/ontology/bibo/pages"));
            pagesMap.CreateObjectMap().IsColumnValued("Pages")
                .HasDataType(new Uri(XmlSpecsHelper.XmlSchemaDataTypeInteger));
        }

        private static void MapFolderCode(ITriplesMapFromR2RMLViewConfiguration sourceMap)
        {
            var codeMap = sourceMap.CreatePropertyObjectMap();
            codeMap.CreatePredicateMap().IsConstantValued(new Uri("http://purl.org/dc/terms/identifier"));
            codeMap.CreateObjectMap().IsColumnValued("FolderCode");
        }

        private static void MapDate(ITriplesMapFromR2RMLViewConfiguration sourceMap)
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

        private static void MapLanguages(ITriplesMapFromR2RMLViewConfiguration sourceMap)
        {
            var langMap = sourceMap.CreatePropertyObjectMap();
            langMap.CreatePredicateMap().IsConstantValued(new Uri("http://purl.org/dc/terms/language"));
            langMap.CreateObjectMap().IsTemplateValued("http://www.lexvo.org/page/iso639-1/{Language}");

            var lang1Map = sourceMap.CreatePropertyObjectMap();
            lang1Map.CreatePredicateMap().IsConstantValued(new Uri("http://purl.org/dc/terms/language"));
            lang1Map.CreateObjectMap().IsTemplateValued("http://www.lexvo.org/page/iso639-1/{Language2}");
        }

        private static void MapType(ITriplesMapFromR2RMLViewConfiguration sourceMap)
        {
            var typeMap = sourceMap.CreatePropertyObjectMap();
            typeMap.CreatePredicateMap().IsConstantValued(new Uri(RdfSpecsHelper.RdfType));
            typeMap.CreateObjectMap().IsTemplateValued("http://wikibus.org/ontology#{Type}");
        }

        private static void MapFolderName(ITriplesMapFromR2RMLViewConfiguration sourceMap)
        {
            var titleMap = sourceMap.CreatePropertyObjectMap();
            titleMap.CreatePredicateMap().IsConstantValued(new Uri("http://purl.org/dc/terms/title"));
            titleMap.CreateObjectMap().IsColumnValued("FolderName");
        }
    }
}
