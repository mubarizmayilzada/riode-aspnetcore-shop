using Riode.WebUI.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Riode.WebUI.AppCode.Extensions
{
    static public partial class Extension
    {
        public static string GetCategoriesRaw(this List<Category> categories)
        {
            if (categories == null || !categories.Any())
            {
                return "";
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("<ul class='widget-body filter-items search-ul'>");

            foreach (var category in categories)
            {
                GetChildrenRaw(category);
            }



            sb.Append("</ul>");
            return sb.ToString();

            void GetChildrenRaw(Category category)
            {

                sb.Append(@"<li>");

                sb.Append(@$"<a href='#'>{category.Name}</a>");
                
                if (category.Children != null && category.Children.Any())
                {
                    sb.Append("<ul>");
                    
                    foreach (var item in category.Children)
                    {

                        GetChildrenRaw(item);
                    
                    }

                    sb.Append("</ul>");
                }

                sb.Append(@"</li>");
            }
        }
    }
}
