using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Reflection;
using System.Text;
using System.Web.Routing;
using System.Linq.Expressions;
using Bonobo.Git.Server.Models;
using System.ComponentModel.DataAnnotations;
using Markdig;
using Markdig.Extensions.JiraLinks;
using Markdig.SyntaxHighlighting;
using Markdig.SemanticUi;

namespace Bonobo.Git.Server.Helpers
{
    public static class CustomHtmlHelpers
    {
        public static IHtmlString AssemblyVersion(this HtmlHelper helper)
        {
            return MvcHtmlString.Create(Assembly.GetExecutingAssembly().GetName().Version.ToString());
        }

        public static IHtmlString MarkdownToHtml(this HtmlHelper helper, string markdownText)
        {
            var pipeline = new MarkdownPipelineBuilder()
                .UseAdvancedExtensions()
                .UsePipeTables()
                .UseCitations()
                .UseEmojiAndSmiley()
                .UseBootstrap()
                .UseSyntaxHighlighting()
                .UseSemanticUi()
                .Build();
            var result = Markdown.ToHtml(markdownText, pipeline);
            // CommonMark.CommonMarkConverter.Convert(markdownText)
            return MvcHtmlString.Create(result);
        }

        public static MvcHtmlString DisplayEnum(this HtmlHelper helper, Enum e)
        {
            string result = "[[" + e.ToString() + "]]";
            var memberInfo = e.GetType().GetMember(e.ToString()).FirstOrDefault();
            if (memberInfo != null)
            {
                var display = memberInfo.GetCustomAttributes(false)
                    .OfType<DisplayAttribute>()
                    .LastOrDefault();

                if (display != null)
                {
                    result = display.GetName();
                }
            }

            return MvcHtmlString.Create(result);
        }
    }
}
