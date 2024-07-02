namespace IRepository
{
    using System.Collections.Generic;

    using POCO;

    public interface IForeignStudentRepository
    {
        void InsertForeignStudent(ForeignStudent foreignStudent, ref List<string> errors);

        void UpdateForeignStudent(ForeignStudent foreignStudent, ref List<string> errors);

        void DeleteForeignStudent(string id, ref List<string> errors);

        ForeignStudent GetForeignStudentDetail(string id, ref List<string> errors);

        List<ForeignStudent> GetForeignStudentList(ref List<string> errors);

        void EnrollSchedule(string studentId, int scheduleId, ref List<string> errors);

        void DropEnrolledSchedule(string studentId, int scheduleId, ref List<string> errors);

        List<Enrollment> GetEnrollments(string studentId);
    }
}
