using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web;

namespace EfDbCoreFirst.Html_Helper
{
    public static class FileHelper
    {
        public static MvcHtmlString File(this HtmlHelper helper,string CssClassName)
        {
            TagBuilder tag = new TagBuilder("input");
            tag.MergeAttribute("type", "file");
            tag.MergeAttribute("id", "Image");
            tag.MergeAttribute("name", "Photo");
            tag.MergeAttribute("class", CssClassName);
            return MvcHtmlString.Create(tag.ToString(TagRenderMode.SelfClosing));
        }
            
     
        
            
    }
}