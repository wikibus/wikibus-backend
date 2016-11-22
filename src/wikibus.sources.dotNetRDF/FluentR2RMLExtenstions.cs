using System;
using TCode.r2rml4net.Mapping.Fluent;

namespace Wikibus.Sources.DotNetRDF
{
    /// <summary>
    /// Simplifies setting up mappings
    /// </summary>
    public static class FluentR2RMLExtenstions
    {
        /// <summary>
        /// Maps a column to predicate with optional data type
        /// </summary>
        public static ITriplesMapConfiguration MapColumn(
            this ITriplesMapConfiguration map,
            string columnName,
            string predicate,
            Uri dataType = null)
        {
            var predicateObjectMap = map.CreatePropertyObjectMap();
            predicateObjectMap.CreatePredicateMap().IsConstantValued(new Uri(predicate));
            var literalTermMapConfiguration = predicateObjectMap.CreateObjectMap().IsColumnValued(columnName);

            if (dataType != null)
            {
                literalTermMapConfiguration.HasDataType(dataType);
            }

            return map;
        }

        /// <summary>
        /// Maps a template to predicate with optional data type
        /// </summary>
        public static ITriplesMapConfiguration MapTemplate(
            this ITriplesMapConfiguration map,
            string template,
            string predicate,
            string dataType = null)
        {
            var predicateObjectMap = map.CreatePropertyObjectMap();
            predicateObjectMap.CreatePredicateMap().IsConstantValued(new Uri(predicate));
            var objectMap = predicateObjectMap.CreateObjectMap().IsTemplateValued(template);

            if (dataType != null)
            {
                ((ILiteralTermMapConfiguration)objectMap.IsLiteral()).HasDataType(dataType);
            }

            return map;
        }

        /// <summary>
        /// Maps a Uri constant to predicate
        /// </summary>
        public static ITriplesMapConfiguration MapConstant(
            this ITriplesMapConfiguration map,
            string value,
            Uri predicate)
        {
            var predicateObjectMap = map.CreatePropertyObjectMap();
            predicateObjectMap.CreatePredicateMap().IsConstantValued(predicate);
            predicateObjectMap.CreateObjectMap().IsConstantValued(value);

            return map;
        }
    }
}
