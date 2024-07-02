namespace Service
{
    using System;
    using System.Collections.Generic;
    using IRepository;
    using POCO;

    public class GradStudentService
    {
        private readonly IGradStudentRepository repository;

        public GradStudentService(IGradStudentRepository repository)
        {
            this.repository = repository;
        }

        public void InsertGradStudent(GradStudent gradStudent, ref List<string> errors)
        {
            if (gradStudent == null)
            {
                errors.Add("GradStudent cannot be null");
                throw new ArgumentException();
            }

            if (gradStudent.Sid.Length < 5)
            {
                errors.Add("Invalid gradStudent ID");
                throw new ArgumentException();
            }

            this.repository.InsertGradStudent(gradStudent, ref errors);
        }

        public void UpdateGradStudent(GradStudent gradStudent, ref List<string> errors)
        {
            if (gradStudent == null)
            {
                errors.Add("GradStudent cannot be null");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(gradStudent.Sid))
            {
                errors.Add("Invalid student id");
                throw new ArgumentException();
            }

            if (gradStudent.Sid.Length < 5)
            {
                errors.Add("Invalid student id");
                throw new ArgumentException();
            }

            this.repository.UpdateGradStudent(gradStudent, ref errors);
        }

        public GradStudent GetGradStudent(string id, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(id))
            {
                errors.Add("Invalid student id");
                throw new ArgumentException();
            }

            return this.repository.GetGradStudentDetail(id, ref errors);
        }

        public void DeleteGradStudent(string id, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(id))
            {
                errors.Add("Invalid student id");
                throw new ArgumentException();
            }

            this.repository.DeleteGradStudent(id, ref errors);
        }

        public List<GradStudent> GetGradStudentList(ref List<string> errors)
        {
            return this.repository.GetGradStudentList(ref errors);
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
