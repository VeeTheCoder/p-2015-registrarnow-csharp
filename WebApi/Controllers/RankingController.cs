namespace WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using IRepository;
    using POCO;
    using Repository;
    using Service;

    public class RankingController : ApiController
    {
        [HttpGet]
        public Ranking GetRankingInfo(string studentId, int scheduleId)
        {
            var errors = new List<string>();
            IRankingRepository ranking = new RankingRepository();
            return new RankingService(ranking).GetRankingInfo(studentId, scheduleId, ref errors);
        }

        [HttpGet]
        public List<Ranking> GetRankingList(int scheduleId)
        {
            var service = new RankingService(new RankingRepository());
            var errors = new List<string>();

            //// we could log the errors here if there are any...
            return service.GetRankings(scheduleId, ref errors);
        }

        [HttpPost]
        public string InsertRanking([FromBody]Ranking rankingInfo)
        {
            var errors = new List<string>();
            IRankingRepository ranking = new RankingRepository();
            new RankingService(ranking).InsertRanking(rankingInfo, ref errors);
            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpPost]
        public void DeleteRanking(string studentId, int scheduleId)
        {
            var errors = new List<string>();
            IRankingRepository ranking = new RankingRepository();
            new RankingService(ranking).DeleteRanking(studentId, scheduleId, ref errors);
        }

        [HttpPost]
        public void UpdateRanking([FromBody]Ranking rankingInfo)
        {
            var errors = new List<string>();
            IRankingRepository ranking = new RankingRepository();
            new RankingService(ranking).UpdateRanking(rankingInfo, ref errors);
        }
    }
}