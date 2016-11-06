using System;
using System.Collections.Generic;
using Tavis.UriTemplates;
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
        public const string BrochuresPath = "brochures";

        /// <summary>
        /// The book path
        /// </summary>
        public const string BookPath = "book/{id}";

        /// <summary>
        /// The books path
        /// </summary>
        public const string BooksPath = "books";

        /// <summary>
        /// The magazine path`
        /// </summary>
        public const string MagazinePath = "magazine/{name}";

        /// <summary>
        /// The magazines path
        /// </summary>
        public const string MagazinesPath = "magazines";

        /// <summary>
        /// The magazine issues path
        /// </summary>
        public const string MagazineIssuesPath = "magazine/{name}/issues";

        /// <summary>
        /// The magazine issue path
        /// </summary>
        public const string MagazineIssuePath = "magazine/{name}/issue/{number}";

        private readonly UriTemplateTable _templateTable = new UriTemplateTable();

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentifierTemplates"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public IdentifierTemplates(IWikibusConfiguration configuration)
        {
            _templateTable.Add(typeof(Book).FullName, new UriTemplate(configuration.BaseResourceNamespace + BookPath));
            _templateTable.Add(typeof(Magazine).FullName, new UriTemplate(configuration.BaseResourceNamespace + MagazinePath));
            _templateTable.Add(typeof(Brochure).FullName, new UriTemplate(configuration.BaseResourceNamespace + BrochurePath));
            _templateTable.Add(typeof(Issue).FullName, new UriTemplate(configuration.BaseResourceNamespace + MagazineIssuePath));
        }

        /// <summary>
        /// Creates the magazine identifier.
        /// </summary>
        /// <param name="name">The name.</param>
        public Uri CreateMagazineIdentifier(string name)
        {
            return new Uri(_templateTable[typeof(Magazine).FullName].AddParameter("name", name).Resolve());
        }

        /// <summary>
        /// Creates the magazine issue identifier.
        /// </summary>
        /// <param name="issueNumber">The issue number.</param>
        /// <param name="magazineName">Name of the magazine.</param>
        public Uri CreateMagazineIssueIdentifier(int issueNumber, string magazineName)
        {
            var uriString = _templateTable[typeof(Issue).FullName]
                .AddParameter("name", magazineName)
                .AddParameter("number", issueNumber).Resolve();

            return new Uri(uriString);
        }

        /// <summary>
        /// Creates the brochure identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public Uri CreateBrochureIdentifier(int id)
        {
            return new Uri(_templateTable[typeof(Brochure).FullName].AddParameter("id", id).Resolve());
        }

        /// <summary>
        /// Creates the book identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public Uri CreateBookIdentifier(int id)
        {
            return new Uri(_templateTable[typeof(Book).FullName].AddParameter("id", id).Resolve());
        }

        /// <summary>
        /// Gets matched parameters for requested template.
        /// </summary>
        /// <typeparam name="T">resource type</typeparam>
        /// <param name="uri">The resource identifier.</param>
        public IDictionary<string, object> GetMatch<T>(Uri uri)
        {
            var templateMatch = _templateTable.Match(uri);

            if (templateMatch.Key == typeof(T).FullName)
            {
                return templateMatch.Parameters;
            }

            return new Dictionary<string, object>();
        }
    }
}