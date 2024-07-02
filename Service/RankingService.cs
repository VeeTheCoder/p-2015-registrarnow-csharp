namespace Service
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using IRepository;
    using POCO;

    public class RankingService
    {
        private readonly IRankingRepository repository;

        public RankingService(IRankingRepository repository)
        {
            this.repository = repository;
        }

        public void InsertRanking(Ranking ranking, ref List<string> errors)
        {
            if (ranking == null)
            {
                errors.Add("Ranking cannot be null");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(ranking.StudentId) || ranking.StudentId.Length < 5)
            {
                errors.Add("Invalid ranking student ID");
                throw new ArgumentException();
            }

            if (ranking.ScheduleId < 0)
            {
                errors.Add("Invalid ranking schedule ID");
            }

            if (ranking.Rank < 0.0)
            {
                errors.Add("Invalid ranking value");
            }

            this.repository.InsertRanking(ranking, ref errors);
        }

        public void UpdateRanking(Ranking ranking, ref List<string> errors)
        {
            if (ranking == null)
            {
                errors.Add("Ranking cannot be null");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(ranking.StudentId) || ranking.StudentId.Length < 5)
            {
                errors.Add("Invalid ranking student ID");
                throw new ArgumentException();
            }

            if (ranking.ScheduleId < 0)
            {
                errors.Add("Invalid ranking schedule ID");
            }

            if (ranking.Rank < 0.0)
            {
                errors.Add("Invalid ranking value");
            }

            this.repository.UpdateRanking(ranking, ref errors);
        }

        public Ranking GetRankingInfo(string studentId, int scheduleId, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(studentId) || studentId.Length < 5)
            {
                errors.Add("Invalid ranking student id");
                throw new ArgumentException();
            }

            if (scheduleId < 0)
            {
                errors.Add("Invalid ranking schedule ID");
            }

            return this.repository.GetRankingInfo(studentId, scheduleId, ref errors);
        }

        public List<Ranking> GetRankings(int scheduleId, ref List<string> errors)
        {
            if (scheduleId < 0)
            {
                errors.Add("Invalid ranking schedule id");
                throw new ArgumentException();
            }

            return this.repository.GetRankings(scheduleId, ref errors);
        }

        public void DeleteRanking(string studentId, int scheduleId, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(studentId) || studentId.Length < 5)
            {
                errors.Add("Invalid ranking student id");
                throw new ArgumentException();
            }

            if (scheduleId < 0)
            {
                errors.Add("Invalid ranking schedule ID");
            }

            this.repository.DeleteRanking(studentId, scheduleId, ref errors);
        }
    }
}
