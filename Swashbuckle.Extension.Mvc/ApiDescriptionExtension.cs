using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Description;

namespace Swashbuckle.Extension.Mvc
{
    public static class ApiDescriptionExtension
    {
        public static void AddRange(this ICollection<ApiDescription> apiDescriptions, IEnumerable<ApiDescription> items)
        {
            if (items != null && items.Any())
            {
                foreach (var item in items)
                {
                    apiDescriptions.Add(item);
                }
            }
        }
    }
}
