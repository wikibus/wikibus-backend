using System;
using System.Collections.Generic;
using System.Linq;
using TunnelVisionLabs.Net;
using Wikibus.Common;

namespace Wikibus.Sources
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
        public const string BooksPath = "books{/page}";

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

        private readonly IDictionary<Type, UriTemplate> templateTable = new Dictionary<Type, UriTemplate>();

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentifierTemplates"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public IdentifierTemplates(IWikibusConfiguration configuration)
        {
            this.templateTable.Add(typeof(Book), new UriTemplate(configuration.BaseResourceNamespace + BookPath));
            this.templateTable.Add(typeof(Magazine), new UriTemplate(configuration.BaseResourceNamespace + MagazinePath));
            this.templateTable.Add(typeof(Brochure), new UriTemplate(configuration.BaseResourceNamespace + BrochurePath));
            this.templateTable.Add(typeof(Issue), new UriTemplate(configuration.BaseResourceNamespace + MagazineIssuePath));
            this.templateTable.Add(typeof(ICollection<Issue>), new UriTemplate(configuration.BaseResourceNamespace + MagazineIssuesPath));
        }

        /// <summary>
        /// Creates the magazine identifier.
        /// </summary>
        /// <param name="name">The name.</param>
        public Uri CreateMagazineIdentifier(string name)
        {
            return this.templateTable[typeof(Magazine)].BindByName(new Dictionary<string, string>
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
            return this.templateTable[typeof(Issue)].BindByName(new Dictionary<string, string>
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
            return this.templateTable[typeof(Brochure)].BindByName(new Dictionary<string, string>
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
            return this.templateTable[typeof(Book)].BindByName(new Dictionary<string, string>
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
            uri = new Uri(Uri.EscapeUriString(uri.ToString()));
            var templateMatch = this.templateTable.First(pair => pair.Value.Match(uri) != null);

            if (templateMatch.Key == typeof(T))
            {
                return templateMatch.Value.Match(uri).Bindings.ToDictionary(m => m.Key, m => m.Value.Value);
            }

            return new Dictionary<string, object>();
        }
    }
}