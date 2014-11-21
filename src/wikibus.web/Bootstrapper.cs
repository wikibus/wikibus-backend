using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using Resourcer;
using Slp.r2rml4net.Storage;
using Slp.r2rml4net.Storage.Sql;
using Slp.r2rml4net.Storage.Sql.Vendor;
using TCode.r2rml4net;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF.Storage;

namespace wikibus.web
{
    /// <summary>
    /// Bootstrapper for wikibus.org API app
    /// </summary>
    public class Bootstrapper : Nancy.DefaultNancyBootstrapper
    {
        /// <summary>
        /// Gets overridden configuration
        /// </summary>
        protected override NancyInternalConfiguration InternalConfiguration
        {
            get { return NancyInternalConfiguration.WithOverrides(c => c.ResponseProcessors = GetProcessors().ToList()); }
        }

        /// <summary>
        /// Configures the container using AutoRegister followed by registration
        /// of default INancyModuleCatalog and IRouteResolver.
        /// </summary>
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            container.Register<ISparqlQueryProcessor, GenericQueryProcessor>();
            container.Register<IQueryableStorage, R2RMLStorage>();
            container.Register<ISqlDb>(CreateDbConnection);
            container.Register(CreateRdbMappings);
        }

        private ISqlDb CreateDbConnection(TinyIoCContainer tinyIoCContainer, NamedParameterOverloads namedParameterOverloads)
        {
            return new MSSQLDb(ConfigurationManager.ConnectionStrings["sql"].ConnectionString);
        }

        private IR2RML CreateRdbMappings(TinyIoCContainer tinyIoCContainer, NamedParameterOverloads namedParameterOverloads)
        {
            var rml = new FluentR2RML();

            var brochureMap = rml.CreateTriplesMapFromR2RMLView(Resource.AsString("Queries.SelectSources.sql"));
            brochureMap.SubjectMap.IsTemplateValued("http://wikibus.org/brochure/{Id}");

            var titleMap = brochureMap.CreatePropertyObjectMap();
            titleMap.CreatePredicateMap().IsConstantValued(new Uri("http://purl.org/dc/terms/title"));
            titleMap.CreateObjectMap().IsColumnValued("FolderName");

            var typeMap = brochureMap.CreatePropertyObjectMap();
            typeMap.CreatePredicateMap().IsConstantValued(new Uri(RdfSpecsHelper.RdfType));
            typeMap.CreateObjectMap().IsTemplateValued("http://wikibus.org/ontology#{Type}");

            return rml;
        }

        private IEnumerable<Type> GetProcessors()
        {
            yield return typeof(Nancy.RDF.Responses.RdfResponseProcessor);
            yield return typeof(Nancy.RDF.Responses.JsonLdResponseProcessor);
        }
    }
}
