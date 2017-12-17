using System;
using System.Collections.Generic;
using Argolis.Models;
using NullGuard;

namespace Wikibus.Sources.EF
{
    public class IdRetriever
    {
        private readonly IUriTemplateMatcher matcher;

        public IdRetriever(IUriTemplateMatcher matcher)
        {
            this.matcher = matcher;
        }

        [return: AllowNull]
        public int? GetBrochureId(Uri uri)
        {
            var uriTemplateMatch = this.matcher.GetMatch<Brochure>(uri);
            if (uriTemplateMatch.Success)
            {
                var binding = uriTemplateMatch["id"];
                return Convert.ToInt32(binding.ToString());
            }

            return null;
        }

        [return: AllowNull]
        public int? GetBookId(Uri uri)
        {
            var uriTemplateMatch = this.matcher.GetMatch<Book>(uri);
            if (uriTemplateMatch.Success)
            {
                var binding = uriTemplateMatch["id"];
                return Convert.ToInt32(binding.ToString());
            }

            return null;
        }

        [return: AllowNull]
        public IssueId GetIssueId(Uri uri)
        {
            var uriTemplateMatch = this.matcher.GetMatch<Issue>(uri);

            if (uriTemplateMatch.Success)
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
            var uriTemplateMatch = this.matcher.GetMatch<Magazine>(uri);

            if (uriTemplateMatch.Success)
            {
                return uriTemplateMatch["name"].ToString();
            }

            return null;
        }

        [return: AllowNull]
        public string GetMagazineForIssuesId(Uri uri)
        {
            var uriTemplateMatch = this.matcher.GetMatch<ICollection<Issue>>(uri);

            if (uriTemplateMatch.Success)
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