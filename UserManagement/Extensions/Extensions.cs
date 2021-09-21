using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace UserManagement.Extensions
{
    public static class Extensions
    {     
        public static MvcHtmlString Button(this HtmlHelper helper, string id,string classAttr, ButtonType type = ButtonType.button, string text = "")
        {
            TagBuilder tag = new TagBuilder("button");
            tag.GenerateId(id);
            tag.AddCssClass(classAttr);
            tag.Attributes.Add(new KeyValuePair<string, string>("type", type.ToString()));
            tag.Attributes.Add(new KeyValuePair<string, string>("name", id));
            tag.SetInnerText(text);
            return MvcHtmlString.Create(tag.ToString());
        }
        
    }

    public enum ButtonType
    {
        button=0,
        submit=1,
        reset=2
    }
}