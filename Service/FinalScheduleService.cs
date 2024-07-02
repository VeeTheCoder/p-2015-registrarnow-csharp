namespace Service
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using IRepository;
    using POCO;

    public class FinalScheduleService
    {
        private readonly IFinalScheduleRepository repository;

        public FinalScheduleService(IFinalScheduleRepository repository)
        {
            this.repository = repository;
        }

        public FinalSchedule GetCourseFinalSchedule(int scheduleId, ref List<string> errors)
        {
            if (scheduleId < 0)
            {
                errors.Add("Invalid Schedule ID");
            }

            return this.repository.GetCourseFinalSchedule(scheduleId, ref errors);
        }

        public void InsertFinalSchedule(FinalSchedule finalSchedule, ref List<string> errors)
        {
            if (finalSchedule == null)
            {
                errors.Add("Final Schedule cannot be null");
                throw new ArgumentException();
            }

            if (finalSchedule.Schedule_id < 0)
            {
                errors.Add("Invalid Schedule ID");
            }

            if (string.IsNullOrEmpty(finalSchedule.FinalLocation))
            {
                errors.Add("Invalid Final Location");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(finalSchedule.Title))
            {
                errors.Add("Invalid Final Location");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(finalSchedule.FinalTime))
            {
                errors.Add("Invalid Final Time");
                throw new ArgumentException();
            }

            finalSchedule.FinalTime = finalSchedule.FinalTime.Replace("..(?!$)", "$0:");
            finalSchedule.FinalTime = finalSchedule.FinalTime + "0000000";

            this.repository.InsertFinalSchedule(finalSchedule, ref errors);
        }

        public void UpdateFinalSchedule(FinalSchedule finalSchedule, ref List<string> errors)
        {
            if (finalSchedule == null)
            {
                errors.Add("Final Schedule cannot be null");
                throw new ArgumentException();
            }

            if (finalSchedule.Schedule_id < 0)
            {
                errors.Add("Invalid Schedule ID");
            }

            if (string.IsNullOrEmpty(finalSchedule.Title))
            {
                errors.Add("Invalid Final Location");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(finalSchedule.FinalLocation))
            {
                errors.Add("Invalid Final Location");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(finalSchedule.FinalTime))
            {
                errors.Add("Invalid Final Time");
                throw new ArgumentException();
            }

            this.repository.UpdateFinalSchedule(finalSchedule, ref errors);
        }

        public List<FinalSchedule> GetFinalSchedule(ref List<string> errors)
        {
            return this.repository.GetFinalSchedule(ref errors);
        }

        public void DeleteSchedule(int scheduleId, ref List<string> errors)
        {
            if (scheduleId < 0)
            {
                errors.Add("Invalid Schedule ID");
            }

            this.repository.DeleteFinalSchedule(scheduleId, ref errors);
        }
    }
}
