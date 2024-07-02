namespace WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using POCO;
    using Repository;
    using Service;

    public class GradStudentController : ApiController
    {
        [HttpGet]
        public List<GradStudent> GetGradStudentList()
        {
            var errors = new List<string>();
            var repository = new GradStudentRepository();
            var service = new GradStudentService(repository);
            return service.GetGradStudentList(ref errors);
        }

        [HttpGet]
        public GradStudent GetGradStudent(string id)
        {
            var errors = new List<string>();
            var repository = new GradStudentRepository();
            var service = new GradStudentService(repository);
            return service.GetGradStudent(id, ref errors);
        }

        [HttpPost]
        public string InsertGradStudent(GradStudent gradStudent)
        {
            var errors = new List<string>();
            var repository = new GradStudentRepository();
            var service = new GradStudentService(repository);
            service.InsertGradStudent(gradStudent, ref errors);
            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpPost]
        public string UpdateGradStudent(GradStudent gradStudent)
        {
            var errors = new List<string>();
            var repository = new GradStudentRepository();
            var service = new GradStudentService(repository);
            service.UpdateGradStudent(gradStudent, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpPost]
        public string DeleteGradStudent(string id)
        {
            var errors = new List<string>();
            var repository = new GradStudentRepository();
            var service = new GradStudentService(repository);
            service.DeleteGradStudent(id, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }
    }
}