using System;
using System.Collections.Generic;
using NullGuard;

namespace Wikibus.Sources.EF
{
    public class IdRetriever
    {
        private readonly IdentifierTemplates configuration;

        public IdRetriever(IdentifierTemplates configuration)
        {
            this.configuration = configuration;
        }

        [return: AllowNull]
        public int? GetBrochureId(Uri uri)
        {
            var uriTemplateMatch = this.configuration.GetMatch<Brochure>(uri);
            if (uriTemplateMatch.ContainsKey("id"))
            {
                var binding = uriTemplateMatch["id"];
                return Convert.ToInt32(binding.ToString());
            }

            return null;
        }

        [return: AllowNull]
        public int? GetBookId(Uri uri)
        {
            var uriTemplateMatch = this.configuration.GetMatch<Book>(uri);
            if (uriTemplateMatch.ContainsKey("id"))
            {
                var binding = uriTemplateMatch["id"];
                return Convert.ToInt32(binding.ToString());
            }

            return null;
        }

        [return: AllowNull]
        public IssueId GetIssueId(Uri uri)
        {
            var uriTemplateMatch = this.configuration.GetMatch<Issue>(uri);

            if (uriTemplateMatch.ContainsKey("name") && uriTemplateMatch.ContainsKey("number"))
            {
                return new IssueId
                {
                    MagazineName = uriTemplateMatch["name"].ToString(),
                    IssueNumber = Convert.ToInt32(uriTemplateMatch["number"].ToString())
                };
            }

            return null;
        }

        [return: AllowNull]
        public string GetMagazineName(Uri uri)
        {
            var uriTemplateMatch = this.configuration.GetMatch<Magazine>(uri);

            if (uriTemplateMatch.ContainsKey("name"))
            {
                return uriTemplateMatch["name"].ToString();
            }

            return null;
        }

        [return: AllowNull]
        public string GetMagazineForIssuesId(Uri uri)
        {
            var uriTemplateMatch = this.configuration.GetMatch<ICollection<Issue>>(uri);

            if (uriTemplateMatch.ContainsKey("name"))
            {
                return uriTemplateMatch["name"].ToString();
            }

            return null;
        }

        public class IssueId
        {
            public string MagazineName { get; set; }

            public int IssueNumber { get; set; }
        }
    }
}