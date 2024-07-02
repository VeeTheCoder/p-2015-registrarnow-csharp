namespace Service
{
    using System;
    using System.Collections.Generic;
    using IRepository;
    using POCO;

    public class ScheduleService
    {
        private readonly IScheduleRepository repository;

        public ScheduleService(IScheduleRepository repository)
        {
            this.repository = repository;
        }

        public void InsertSchedule(Schedule schedule, ref List<string> errors)
        {
            if (schedule == null)
            {
                errors.Add("Schedule cannot be null");
                throw new ArgumentException();
            }

            if (schedule.ScheduleId < 0)
            {
                errors.Add("Invalid schedule ID");
                throw new ArgumentException();
            }

            if (schedule.Year == null || schedule.Quarter == null || schedule.Session == null || schedule.Course == null)
            {
                errors.Add("Invalid schedule field");
                throw new ArgumentException();
            }

            this.repository.InsertSchedule(schedule, ref errors);
        }

        public List<Schedule> GetScheduleList(string year, string quarter, ref List<string> errors)
        {
            return this.repository.GetScheduleList(year, quarter, ref errors);
        }
    }
}
