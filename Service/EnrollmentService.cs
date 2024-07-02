namespace Service
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using IRepository;
    using POCO;

    public class EnrollmentService
    {
        // Regex pattern for grade matching (pluses and minuses allowed)
        private const string PATTERN = @"^[A-DF][+-]?$";

        private readonly IEnrollmentRepository repository;

        public EnrollmentService(IEnrollmentRepository repository)
        {
            this.repository = repository;
        }

        public void InsertEnrollment(Enrollment enrollment, ref List<string> errors)
        {
            if (enrollment == null)
            {
                errors.Add("Enrollment cannot be null");
                throw new ArgumentException();
            }

            if (enrollment.Grade != null)
            {
                Regex rgx = new Regex(PATTERN);

                MatchCollection matches = rgx.Matches(enrollment.Grade);

                if (!(matches.Count > 0))
                {
                    errors.Add("Enrollment grade has an improper format");
                    throw new ArgumentException();
                }
            }

            if (string.IsNullOrEmpty(enrollment.StudentId) || enrollment.StudentId.Length < 5)
            {
                errors.Add("Invalid enrollment student ID");
                throw new ArgumentException();
            }

            if (enrollment.ScheduleId < 0) 
            {
                errors.Add("Invalid enrollment schedule ID");
            }

            if (enrollment.GradeValue < 0.0)
            {
                errors.Add("Invalid enrollment GradeValue");
            }

            this.repository.InsertEnrollment(enrollment, ref errors);
        }

        public void UpdateEnrollment(Enrollment enrollment, ref List<string> errors)
        {
            if (enrollment == null)
            {
                errors.Add("Enrollment cannot be null");
                throw new ArgumentException();
            }

            Regex rgx = new Regex(PATTERN);

            MatchCollection matches = rgx.Matches(enrollment.Grade);

            if (!(matches.Count > 0))
            {
                errors.Add("Enrollment grade has an improper format");
            }

            if (string.IsNullOrEmpty(enrollment.StudentId) || enrollment.StudentId.Length < 5)
            {
                errors.Add("Invalid enrollment student ID");
                throw new ArgumentException();
            }

            if (enrollment.ScheduleId < 0)
            {
                errors.Add("Invalid enrollment schedule ID");
            }

            if (enrollment.GradeValue < 0.0)
            {
                errors.Add("Invalid enrollment GradeValue");
            }

            this.repository.UpdateEnrollment(enrollment, ref errors);
        }

        public List<Enrollment> GetEnrollmentInfo(string studentId, int scheduleId, ref List<string> errors)
        {
            if (scheduleId < 0)
            {
                errors.Add("Invalid enrollment schedule ID");
            }

            return this.repository.GetEnrollmentInfo(studentId, scheduleId, ref errors);
        }

        public void DeleteEnrollment(string studentId, int scheduleId, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(studentId) || studentId.Length < 5)
            {
                errors.Add("Invalid enrollment student id");
                throw new ArgumentException();
            }

            if (scheduleId < 0)
            {
                errors.Add("Invalid enrollment schedule ID");
            }

            this.repository.DeleteEnrollment(studentId, scheduleId, ref errors);
        }
    }
}
