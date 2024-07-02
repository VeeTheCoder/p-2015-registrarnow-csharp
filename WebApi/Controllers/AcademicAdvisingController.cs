namespace WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using IRepository;
    using POCO;
    using Repository;
    using Service;

    public class AcademicAdvisingController : ApiController
    {
        [HttpGet]
        public AcademicAdvisingMessage GetAdvisingMessageDetail(int id)
        {
            var errors = new List<string>();
            IAcademicAdvisingRepository advising = new AcademicAdvisingRepository();
            return new AcademicAdvisingService(advising).GetAdvisingMessageDetail(id, ref errors);
        }

        [HttpPost]
        public string InsertAdvisingMessage(AcademicAdvisingMessage message)
        {
            var errors = new List<string>();
            IAcademicAdvisingRepository advising = new AcademicAdvisingRepository();
            new AcademicAdvisingService(advising).InsertAdvisingMessage(message, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpPost]
        public void UpdateAdvisingMessage(AcademicAdvisingMessage message)
        {
            var errors = new List<string>();
            IAcademicAdvisingRepository advising = new AcademicAdvisingRepository();
            new AcademicAdvisingService(advising).UpdateAdvisingMessage(message, ref errors);
        }

        [HttpPost]
        public string DeleteAdvisingMessage(int id)
        {
            var errors = new List<string>();
            IAcademicAdvisingRepository advising = new AcademicAdvisingRepository();
            new AcademicAdvisingService(advising).DeleteAdvisingMessage(id, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpGet]
        public List<AcademicAdvisingMessage> GetAdvisingMessages(string studentId, bool isInstructor)
        {
            var errors = new List<string>();
            IAcademicAdvisingRepository advising = new AcademicAdvisingRepository();
            return new AcademicAdvisingService(advising).GetAdvisingMessages(studentId, isInstructor, ref errors);
        }
    }
}