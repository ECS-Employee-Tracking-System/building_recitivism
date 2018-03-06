using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorProjectECS.Library
{
    [HtmlTargetElement(Attributes = "AdminOnly")]
    public class AdminTagHelper : TagHelper
    {
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // Redirect to home page if we are not authenticated
            var accessLevel = ViewContext.HttpContext.Session.GetInt32("AccessLevel");
            if (accessLevel == null || accessLevel > 1)
            {
                output.SuppressOutput();
            }
        }
    }
}
