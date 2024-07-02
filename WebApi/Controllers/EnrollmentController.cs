namespace WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using IRepository;
    using POCO;
    using Repository;
    using Service;

    public class EnrollmentController : ApiController
    {
        [HttpGet]
        public List<Enrollment> GetEnrollmentInfo(string studentId, int scheduleId)
        {
            var errors = new List<string>();
            IEnrollmentRepository enrollment = new EnrollmentRepository();
            return new EnrollmentService(enrollment).GetEnrollmentInfo(studentId, scheduleId, ref errors);
        }

        [HttpGet]
        public List<Enrollment> GetEnrollmentInfo(int scheduleId)
        {
            var errors = new List<string>();
            IEnrollmentRepository enrollment = new EnrollmentRepository();
            return new EnrollmentService(enrollment).GetEnrollmentInfo(string.Empty, scheduleId, ref errors);
        }

        [HttpPost]
        public string InsertEnrollment([FromBody]Enrollment enrollmentInfo)
        {
            var errors = new List<string>();
            IEnrollmentRepository enrollment = new EnrollmentRepository();
            new EnrollmentService(enrollment).InsertEnrollment(enrollmentInfo, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpPost]
        public string DeleteEnrollment(string studentId, int scheduleId)
        {
            var errors = new List<string>();
            IEnrollmentRepository enrollment = new EnrollmentRepository();
            new EnrollmentService(enrollment).DeleteEnrollment(studentId, scheduleId, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpPost]
        public void UpdateEnrollment([FromBody]Enrollment enrollmentInfo)
        {
            var errors = new List<string>();
            IEnrollmentRepository enrollment = new EnrollmentRepository();
            new EnrollmentService(enrollment).UpdateEnrollment(enrollmentInfo, ref errors);
        }
    }
}