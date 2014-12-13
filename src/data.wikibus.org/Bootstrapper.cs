﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using JsonLD.Entities;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Diagnostics;
using Nancy.RDF.Responses;
using Nancy.TinyIoc;
using Slp.r2rml4net.Storage;
using Slp.r2rml4net.Storage.Sql;
using Slp.r2rml4net.Storage.Sql.Vendor;
using TCode.r2rml4net;
using VDS.RDF.Query;
using VDS.RDF.Storage;
using wikibus.sources;
using wikibus.sources.dotNetRDF;

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
            get
            {
                return NancyInternalConfiguration.WithOverrides(c =>
                {
                    c.ResponseProcessors = c.ResponseProcessors.Where(IsNotNancyProcessor).ToList();
                });
            }
        }

        /// <summary>
        /// Gets the diagnostics configuration.
        /// </summary>
        /// <value>
        /// The diagnostics configuration.
        /// </value>
        protected override DiagnosticsConfiguration DiagnosticsConfiguration
        {
            get { return new DiagnosticsConfiguration { Password = @"wb" }; }
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
            container.Register<ISqlDb>(new MSSQLDb(ConfigurationManager.ConnectionStrings["sql"].ConnectionString));
            container.Register<IR2RML, WikibusR2RML>();

            var contextProvider = new StaticContextProvider();
            contextProvider.SetupSourcesContexts();
            container.Register<IContextProvider>(contextProvider);
        }

        /// <summary>
        /// Applications the startup.
        /// </summary>
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
#if DEBUG
            StaticConfiguration.DisableErrorTraces = false;
#endif
        }

        private static bool IsNotNancyProcessor(Type responseProcessor)
        {
            return responseProcessor.Assembly == typeof(INancyBootstrapper).Assembly;
        }
    }
}