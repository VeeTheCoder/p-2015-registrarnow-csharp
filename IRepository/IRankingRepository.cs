namespace IRepository
{
    using System.Collections.Generic;

    using POCO;

    public interface IRankingRepository
    {
        void InsertRanking(Ranking ranking, ref List<string> errors); // C

        Ranking GetRankingInfo(string studentId, int scheduleId, ref List<string> errors); // R

        void UpdateRanking(Ranking ranking, ref List<string> errors); // U

        void DeleteRanking(string studentId, int scheduleId, ref List<string> errors); // D

        List<Ranking> GetRankings(int scheduleId, ref List<string> errors); // R
    }
}
