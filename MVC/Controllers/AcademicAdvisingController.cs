namespace MVC.Controllers
{
    using System.Web.Mvc;

    public class AcademicAdvisingController : Controller
    {
        public ActionResult Index(string studentId, bool isInstructor)
        {
            ViewBag.studentId = studentId;
            ViewBag.isInstructor = isInstructor;
            return this.View();
        }

        public ActionResult Get(int id)
        {
            ViewBag.id = id;
            return this.View();
        }

        public ActionResult Delete(int id)
        {
            ViewBag.id = id;
            return this.View();
        }

        public ActionResult Create()
        {
            return this.View();
        }

        public ActionResult Update(int id)
        {
            ViewBag.id = id;
            return this.View();
        }

        public ActionResult Respond(int id)
        {
            ViewBag.id = id;
            return this.View();
        }
    }
}
