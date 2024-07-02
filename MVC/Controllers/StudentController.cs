namespace MVC.Controllers
{
    using System.Web.Mvc;

    public class StudentController : Controller
    {
        public ActionResult Index(string id)
        {
            ViewBag.Id = id;
            return this.View();
        }

        public ActionResult Edit(string id)
        {
            ViewBag.Id = id;
            return this.View();
        }

        public ActionResult AcademicAdvisingList(string id)
        {
            ViewBag.id = id;
            return this.View();
        }

        public ActionResult AcademicAdvisingCreate(string id)
        {
            ViewBag.studentId = id;
            return this.View();
        }

        public ActionResult AcademicAdvisingRespond(int id)
        {
            ViewBag.id = id;
            return this.View();
        }

        public ActionResult AcademicAdvisingGet(int id)
        {
            ViewBag.id = id;
            return this.View();
        }

        public ActionResult GradeChangeList(string id)
        {
            ViewBag.studentId = id;
            return this.View();
        }

        public ActionResult GradeChangeCreate(string id)
        {
            ViewBag.studentId = id;
            return this.View();
        }

        public ActionResult GradeChangeGet(int id)
        {
            ViewBag.id = id;
            return this.View();
        }

        public ActionResult Transcript(string id)
        {
            ViewBag.Id = id;
            return this.View();
        }
    }
}
