namespace MVC.Controllers
{
    using System.Web.Mvc;

    public class ForeignStudentController : Controller
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

        public ActionResult CreateForeignStudent(string id)
        {
            ViewBag.Id = id;
            return this.View();
        }

        public ActionResult DeleteForeignStudent(string id)
        {
            ViewBag.Id = id;
            return this.View();
        }
    }
}
