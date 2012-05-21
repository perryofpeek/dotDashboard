using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

using dotDash.Domain;

namespace dotDashboard.Controllers
{
    public class RRApiController : Controller
    {
        //
        // GET: /Api/

        public ActionResult Index()
        {
            throw new NotImplementedException();
        }

        // GET /Customers
        // Return all customers.
        [HttpGet, OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public ViewResult Index(string verb)
        {
            var serviceEvent = new ServiceEvent { State = "down" };            
            return View("Index",serviceEvent);
        }

        public ViewResult Details(string name)
        {
            var serviceEvent = new ServiceEvent { State = "up" };

            return View("Details",serviceEvent);
        }
    }
}
