namespace IRepository
{
    using System.Collections.Generic;

    using POCO;

    public interface IFinalScheduleRepository
    {
        void InsertFinalSchedule(FinalSchedule finalSchedule, ref List<string> errors); // C

        List<FinalSchedule> GetFinalSchedule(ref List<string> errors); // R

        void UpdateFinalSchedule(FinalSchedule finalSchedule, ref List<string> errors); // U

        void DeleteFinalSchedule(int scheduleId, ref List<string> errors); // D

        FinalSchedule GetCourseFinalSchedule(int scheduleId, ref List<string> errors);
    }
}
