using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hello1.Controllers
{
    public class RentalController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New()
        {
            return View();
        }

        [HttpPut]
        public ActionResult Update()
        {
            return View();
        }

    }
}