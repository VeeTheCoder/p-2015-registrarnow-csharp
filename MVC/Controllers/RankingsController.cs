namespace MVC.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    
    public class RankingsController : Controller
    {
        // GET: Rankings
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult ManageRankings(string id, string course)
        {
            ViewBag.Id = id;
            ViewBag.Course = course;
            return this.View();
        }
    }
}