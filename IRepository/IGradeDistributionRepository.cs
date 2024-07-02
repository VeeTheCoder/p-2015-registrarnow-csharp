namespace IRepository
{
    using System.Collections.Generic;

    using POCO;

    public interface IGradeDistributionRepository
    {
        void InsertGradeDistribution(GradeDistribution gradeDistribution, ref List<string> errors); // C

        List<GradeDistribution> GetGradeDistribution(ref List<string> errors); // R

        void UpdateGradeDistribution(GradeDistribution gradeDistribution, ref List<string> errors); // U

        void DeleteGradeDistribution(int scheduleId, ref List<string> errors); // D

        GradeDistribution GetCourseGradeDistribution(int scheduleId, ref List<string> errors);
    }
}
