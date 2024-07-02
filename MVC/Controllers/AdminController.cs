namespace MVC.Controllers
{
    using System.Web.Mvc;

    public class AdminController : Controller
    {
        public ActionResult Index(int id)
        {
            ViewBag.id = id;
            return this.View();
        }

        [HttpPost]
        public ActionResult Index(int id, string year, string quarter)
        {
            ViewBag.id = id;
            Globals.CurrentYear = year;
            Globals.CurrentQuarter = quarter;
            return this.View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.id = id;
            return this.View();
        }

        public ActionResult StudentList()
        {
            return this.View();
        }

        public ActionResult AcademicAdvisingList(int id)
        {
            ViewBag.id = id;
            return this.View();
        }

        public ActionResult AcademicAdvisingGet(int id)
        {
            ViewBag.id = id;
            return this.View();
        }

        public ActionResult GradeChangeList(int id)
        {
            ViewBag.id = id;
            return this.View();
        }

        public ActionResult GradeChangeGet(int id)
        {
            ViewBag.id = id;
            return this.View();
        }

        public ActionResult ManageFinalSchedule()
        {
            return this.View();
        }

        public ActionResult ManageGradeDistribution()
        {
            return this.View();
        }
    }
}
