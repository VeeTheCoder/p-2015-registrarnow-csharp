namespace IRepository
{
    using System.Collections.Generic;

    using POCO;

    public interface IGradStudentRepository
    {
        void InsertGradStudent(GradStudent gradStudent, ref List<string> errors);

        void UpdateGradStudent(GradStudent gradStudent, ref List<string> errors);

        void DeleteGradStudent(string id, ref List<string> errors);

        GradStudent GetGradStudentDetail(string id, ref List<string> errors);

        List<GradStudent> GetGradStudentList(ref List<string> errors);

        void EnrollSchedule(string studentId, int scheduleId, ref List<string> errors);

        void DropEnrolledSchedule(string studentId, int scheduleId, ref List<string> errors);

        List<Enrollment> GetEnrollments(string studentId);
    }
}
