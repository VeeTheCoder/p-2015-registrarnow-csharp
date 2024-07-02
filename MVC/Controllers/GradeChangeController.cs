namespace MVC.Controllers
{
    using System.Web.Mvc;

    public class GradeChangeController : Controller
    {
        public ActionResult Index(int id)
        {
            ViewBag.id = id;
            return this.View();
        }
    }
}
