namespace Service
{
    using System;
    using System.Collections.Generic;
    using IRepository;
    using POCO;

    public class ForeignStudentService
    {
        private readonly IForeignStudentRepository repository;

        public ForeignStudentService(IForeignStudentRepository repository)
        {
            this.repository = repository;
        }

        public void InsertForeignStudent(ForeignStudent foreignStudent, ref List<string> errors)
        {
            if (foreignStudent == null)
            {
                errors.Add("ForeignStudent cannot be null");
                throw new ArgumentException();
            }

            if (foreignStudent.Sid.Length < 5)
            {
                errors.Add("Invalid foreignStudent ID");
                throw new ArgumentException();
            }

            this.repository.InsertForeignStudent(foreignStudent, ref errors);
        }

        public void UpdateForeignStudent(ForeignStudent foreignStudent, ref List<string> errors)
        {
            if (foreignStudent == null)
            {
                errors.Add("ForeignStudent cannot be null");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(foreignStudent.Sid))
            {
                errors.Add("Invalid student id");
                throw new ArgumentException();
            }

            if (foreignStudent.Sid.Length < 5)
            {
                errors.Add("Invalid student id");
                throw new ArgumentException();
            }

            this.repository.UpdateForeignStudent(foreignStudent, ref errors);
        }

        public ForeignStudent GetForeignStudent(string id, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(id))
            {
                errors.Add("Invalid student id");
                throw new ArgumentException();
            }

            return this.repository.GetForeignStudentDetail(id, ref errors);
        }

        public void DeleteForeignStudent(string id, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(id))
            {
                errors.Add("Invalid student id");
                throw new ArgumentException();
            }

            this.repository.DeleteForeignStudent(id, ref errors);
        }

        public List<ForeignStudent> GetForeignStudentList(ref List<string> errors)
        {
            return this.repository.GetForeignStudentList(ref errors);
        }

        public void EnrollSchedule(string studentId, int scheduleId, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(studentId) || scheduleId < 0)
            {
                errors.Add("Invalid student id or schedule id");
                throw new ArgumentException();
            }

            this.repository.EnrollSchedule(studentId, scheduleId, ref errors);
        }

        public void DropEnrolledSchedule(string studentId, int scheduleId, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(studentId) || scheduleId < 0)
            {
                errors.Add("Invalid student id or schedule id");
                throw new ArgumentException();
            }

            this.repository.DropEnrolledSchedule(studentId, scheduleId, ref errors);
        }

        public List<Enrollment> GetEnrollments(string studentId, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(studentId))
            {
                errors.Add("Invalid student id");
                throw new ArgumentException();
            }

            return this.repository.GetEnrollments(studentId);
        }

        public float CalculateGpa(string studentId, List<Enrollment> enrollments, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(studentId))
            {
                errors.Add("Invalid student id");
                throw new ArgumentException();
            }

            if (enrollments == null)
            {
                errors.Add("Invalid student id");
                throw new ArgumentException();
            }

            if (enrollments.Count == 0)
            {
                return 0.0f;
            }

            var sum = 0.0f;

            foreach (var enrollment in enrollments)
            {
                sum += enrollment.GradeValue;
            }

            return sum / enrollments.Count;
        }
    }
}
