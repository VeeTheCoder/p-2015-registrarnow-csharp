namespace WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using IRepository;
    using POCO;
    using Repository;
    using Service;

    public class GradeChangeController : ApiController
    {
        [HttpGet]
        public GradeChangeMessage GetGradeChangeMessageDetail(int id)
        {
            var errors = new List<string>();
            IGradeChangeRepository gradeChange = new GradeChangeRepository();
            return new GradeChangeService(gradeChange).GetGradeChangeMessageDetail(id, ref errors);
        }

        [HttpPost]
        public string InsertGradeChangeMessage(GradeChangeMessage message)
        {
            var errors = new List<string>();
            IGradeChangeRepository gradeChange = new GradeChangeRepository();
            new GradeChangeService(gradeChange).InsertGradeChangeMessage(message, ref errors);
            
            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpPost]
        public void UpdateGradeChangeMessage([FromBody]GradeChangeMessage message)
        {
            var errors = new List<string>();
            IGradeChangeRepository gradeChange = new GradeChangeRepository();
            new GradeChangeService(gradeChange).UpdateGradeChangeMessage(message, ref errors);
        }

        [HttpPost]
        public string DeleteGradeChangeMessage(int id)
        {
            var errors = new List<string>();
            IGradeChangeRepository gradeChange = new GradeChangeRepository();
            new GradeChangeService(gradeChange).DeleteGradeChangeMessage(id, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpGet]
        public List<GradeChangeMessage> GetGradeChangeMessages(string studentId, bool isInstructor)
        {
            var errors = new List<string>();
            IGradeChangeRepository gradeChange = new GradeChangeRepository();
            return new GradeChangeService(gradeChange).GetGradeChangeMessages(studentId, isInstructor, ref errors);
        }
    }
}