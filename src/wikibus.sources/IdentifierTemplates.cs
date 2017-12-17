using System;
using System.Collections.Generic;
using Argolis.Models;

namespace Wikibus.Sources
{
    /// <summary>
    /// Helps managing URI templates of resources
    /// </summary>
    public static class IdentifierTemplates
    {
        /// <summary>
        /// Creates the magazine identifier.
        /// </summary>
        public static Uri CreateMagazineIdentifier(this IUriTemplateExpander templateExpander, string name)
        {
            return templateExpander.ExpandAbsolute<Magazine>(new Dictionary<string, object>
            {
                { "name", name },
            });
        }

        /// <summary>
        /// Creates the magazine issue identifier.
        /// </summary>
        public static Uri CreateMagazineIssueIdentifier(this IUriTemplateExpander templateExpander, int issueNumber, string magazineName)
        {
            return templateExpander.ExpandAbsolute<Issue>(new Dictionary<string, object>
            {
                { "name", magazineName },
                { "number", issueNumber.ToString() }
            });
        }

        /// <summary>
        /// Creates the brochure identifier.
        /// </summary>
        public static Uri CreateBrochureIdentifier(this IUriTemplateExpander templateExpander, int id)
        {
            return templateExpander.ExpandAbsolute<Brochure>(new Dictionary<string, object>
            {
                { "id", id.ToString() }
            });
        }

        /// <summary>
        /// Creates the book identifier.
        /// </summary>
        public static Uri CreateBookIdentifier(this IUriTemplateExpander templateExpander, int id)
        {
            return templateExpander.ExpandAbsolute<Book>(new Dictionary<string, object>
            {
                { "id", id.ToString() }
            });
        }

        /// <summary>
        /// Gets matched parameters for requested template.
        /// </summary>
        public static UriTemplateMatches GetMatch<T>(this IUriTemplateMatcher templateMatcher, Uri uri)
        {
            return templateMatcher.Match<T>(uri);
        }
    }
}