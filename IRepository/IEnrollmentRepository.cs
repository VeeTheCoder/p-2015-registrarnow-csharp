namespace IRepository
{
    using System.Collections.Generic;

    using POCO;

    public interface IEnrollmentRepository
    {
        void InsertEnrollment(Enrollment enrollment, ref List<string> errors); // C

        List<Enrollment> GetEnrollmentInfo(string studentId, int scheduleId, ref List<string> errors); // R

        void UpdateEnrollment(Enrollment enrollment, ref List<string> errors); // U

        void DeleteEnrollment(string studentId, int scheduleId, ref List<string> errors); // D
    }
}
