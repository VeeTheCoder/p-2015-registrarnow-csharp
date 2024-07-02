namespace Service
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using IRepository;
    using POCO;

    public class GradeDistributionService
    {
        private readonly IGradeDistributionRepository repository;

        public GradeDistributionService(IGradeDistributionRepository repository)
        {
            this.repository = repository;
        }

        public GradeDistribution GetCourseGradeDistribution(int scheduleId, ref List<string> errors)
        {
            if (scheduleId < 0)
            {
                errors.Add("Invalid Schedule ID");
            }

            return this.repository.GetCourseGradeDistribution(scheduleId, ref errors);
        }

        public void InsertGradeDistribution(GradeDistribution gradeDistribution, ref List<string> errors)
        {
            if (gradeDistribution == null)
            {
                errors.Add("Grade Distribution cannot be null");
                throw new ArgumentException();
            }

            if (gradeDistribution.Schedule_id < 0)
            {
                errors.Add("Invalid Schedule ID");
            }

            if (string.IsNullOrEmpty(gradeDistribution.Grade_Distribution))
            {
                errors.Add("Invalid Grade Distribution");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(gradeDistribution.Title))
            {
                errors.Add("Invalid Grade Distribution");
                throw new ArgumentException();
            }

            this.repository.InsertGradeDistribution(gradeDistribution, ref errors);
        }

        public void UpdateGradeDistribution(GradeDistribution gradeDistribution, ref List<string> errors)
        {
            if (gradeDistribution == null)
            {
                errors.Add("Grade Distribution cannot be null");
                throw new ArgumentException();
            }

            if (gradeDistribution.Schedule_id < 0)
            {
                errors.Add("Invalid Schedule ID");
            }

            if (string.IsNullOrEmpty(gradeDistribution.Grade_Distribution))
            {
                errors.Add("Invalid Grade Distribution");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(gradeDistribution.Title))
            {
                errors.Add("Invalid Grade Distribution");
                throw new ArgumentException();
            }

            this.repository.UpdateGradeDistribution(gradeDistribution, ref errors);
        }

        public List<GradeDistribution> GetGradeDistribution(ref List<string> errors)
        {
            return this.repository.GetGradeDistribution(ref errors);
        }

        public void DeleteSchedule(int scheduleId, ref List<string> errors)
        {
            if (scheduleId < 0)
            {
                errors.Add("Invalid Schedule ID");
            }

            this.repository.DeleteGradeDistribution(scheduleId, ref errors);
        }
    }
}
