using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DefaultMvcProject.Controllers
{
    public class ValuesController : Controller
    {
        // GET: Values
        public ActionResult Get()
        {
            return Json(new List<string>() { "Swagger", ".NET Core" });
        }

        // POST：Revert
        public ActionResult Reverse(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new Exception("Parameter \"value\" can't be null");
            return Json(new { Data = new string(value.Reverse().ToArray()), Flag = true });
        }
    }
}