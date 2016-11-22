﻿using System;
using System.Collections.Generic;
using System.Linq;
using TunnelVisionLabs.Net;
using wikibus.common;

namespace wikibus.sources
{
    /// <summary>
    /// Helps managing URI templates of resources
    /// </summary>
    public class IdentifierTemplates
    {
        /// <summary>
        /// The brochure path
        /// </summary>
        public const string BrochurePath = "brochure/{id}";

        /// <summary>
        /// The brochures path
        /// </summary>
        public const string BrochuresPath = "brochures{?page}";

        /// <summary>
        /// The book path
        /// </summary>
        public const string BookPath = "book/{id}";

        /// <summary>
        /// The books path
        /// </summary>
        public const string BooksPath = "books{/page}{?title,author}";

        /// <summary>
        /// The magazine path`
        /// </summary>
        public const string MagazinePath = "magazine/{name}";

        /// <summary>
        /// The magazines path
        /// </summary>
        public const string MagazinesPath = "magazines{?page}";

        /// <summary>
        /// The magazine issues path
        /// </summary>
        public const string MagazineIssuesPath = "magazine/{name}/issues";

        /// <summary>
        /// The magazine issue path
        /// </summary>
        public const string MagazineIssuePath = "magazine/{name}/issue/{number}";

        private readonly IDictionary<Type, UriTemplate> _templateTable = new Dictionary<Type, UriTemplate>();

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentifierTemplates"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public IdentifierTemplates(IWikibusConfiguration configuration)
        {
            _templateTable.Add(typeof(Book), new UriTemplate(configuration.BaseResourceNamespace + BookPath));
            _templateTable.Add(typeof(Magazine), new UriTemplate(configuration.BaseResourceNamespace + MagazinePath));
            _templateTable.Add(typeof(Brochure), new UriTemplate(configuration.BaseResourceNamespace + BrochurePath));
            _templateTable.Add(typeof(Issue), new UriTemplate(configuration.BaseResourceNamespace + MagazineIssuePath));
        }

        /// <summary>
        /// Creates the magazine identifier.
        /// </summary>
        /// <param name="name">The name.</param>
        public Uri CreateMagazineIdentifier(string name)
        {
            return _templateTable[typeof(Magazine)].BindByName(new Dictionary<string, string>
            {
                { "name", name },
            });
        }

        /// <summary>
        /// Creates the magazine issue identifier.
        /// </summary>
        /// <param name="issueNumber">The issue number.</param>
        /// <param name="magazineName">Name of the magazine.</param>
        public Uri CreateMagazineIssueIdentifier(int issueNumber, string magazineName)
        {
            return _templateTable[typeof(Issue)].BindByName(new Dictionary<string, string>
            {
                { "name", magazineName },
                { "number", issueNumber.ToString() }
            });
        }

        /// <summary>
        /// Creates the brochure identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public Uri CreateBrochureIdentifier(int id)
        {
            return _templateTable[typeof(Brochure)].BindByName(new Dictionary<string, string>
            {
                { "id", id.ToString() }
            });
        }

        /// <summary>
        /// Creates the book identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public Uri CreateBookIdentifier(int id)
        {
            return _templateTable[typeof(Book)].BindByName(new Dictionary<string, string>
            {
                { "id", id.ToString() }
            });
        }

        /// <summary>
        /// Gets matched parameters for requested template.
        /// </summary>
        /// <typeparam name="T">resource type</typeparam>
        /// <param name="uri">The resource identifier.</param>
        public IDictionary<string, object> GetMatch<T>(Uri uri)
        {
            var templateMatch = _templateTable.First(pair => pair.Value.Match(uri) != null);

            if (templateMatch.Key == typeof(T))
            {
                return templateMatch.Value.Match(uri).Bindings.ToDictionary(m => m.Key, m => m.Value.Value);
            }

            return new Dictionary<string, object>();
        }
    }
}