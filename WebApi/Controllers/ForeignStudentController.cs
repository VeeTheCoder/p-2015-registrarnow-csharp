namespace WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using POCO;
    using Repository;
    using Service;

    public class ForeignStudentController : ApiController
    {
        [HttpGet]
        public List<ForeignStudent> GetForeignStudentList()
        {
            var errors = new List<string>();
            var repository = new ForeignStudentRepository();
            var service = new ForeignStudentService(repository);
            return service.GetForeignStudentList(ref errors);
        }

        [HttpGet]
        public ForeignStudent GetForeignStudent(string id)
        {
            var errors = new List<string>();
            var repository = new ForeignStudentRepository();
            var service = new ForeignStudentService(repository);
            return service.GetForeignStudent(id, ref errors);
        }

        [HttpPost]
        public string InsertForeignStudent(ForeignStudent foreignStudent)
        {
            var errors = new List<string>();
            var repository = new ForeignStudentRepository();
            var service = new ForeignStudentService(repository);
            service.InsertForeignStudent(foreignStudent, ref errors);
            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpPost]
        public string UpdateForeignStudent(ForeignStudent foreignStudent)
        {
            var errors = new List<string>();
            var repository = new ForeignStudentRepository();
            var service = new ForeignStudentService(repository);
            service.UpdateForeignStudent(foreignStudent, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpPost]
        public string DeleteForeignStudent(string id)
        {
            var errors = new List<string>();
            var repository = new ForeignStudentRepository();
            var service = new ForeignStudentService(repository);
            service.DeleteForeignStudent(id, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }
    }
}