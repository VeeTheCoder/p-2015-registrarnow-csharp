// <copyright file="RankingTest.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
namespace WebApiTest
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using POCO;

    using WebApi.Controllers;

    /// <summary>
    /// Authorize test cases
    /// </summary>
    [TestClass]
    public class RankingTest
    {
        /// <summary>
        /// Authorize test cases
        /// </summary>
        [TestMethod]
        public void RankingGetTest()
        {
            var rankingController = new RankingController();
            var ranking = rankingController.GetRankingInfo("A000003", 100);
            Assert.AreEqual("A000003", ranking.StudentId);
            Assert.AreEqual(100, ranking.ScheduleId);
            Assert.AreEqual(2, ranking.Rank);
        }

        /// <summary>
        /// Authorize test cases
        /// </summary>
        [TestMethod]
        public void RankingInsertDeletePostTest()
        {
            try
            {
                var rankingController = new RankingController();

                rankingController.DeleteRanking("A000009", 109);

                var ranking = rankingController.GetRankingInfo("A000009", 109);
                Assert.AreEqual(null, ranking);

                rankingController.InsertRanking(new Ranking
                {
                    StudentId = "A000009",
                    ScheduleId = 109,
                    Rank = 20
                });

                ranking = rankingController.GetRankingInfo("A000009", 109);
                Assert.AreEqual("A000009", ranking.StudentId);
                Assert.AreEqual(109, ranking.ScheduleId);
                Assert.AreEqual(20, ranking.Rank);

                rankingController.DeleteRanking("A000009", 109);

                ranking = rankingController.GetRankingInfo("A000009", 109);
                Assert.AreEqual(null, ranking);
            }
            catch 
            { 
                throw; 
            }
        }

        /// <summary>
        /// Authorize test cases
        /// </summary>
        [TestMethod]
        public void RankingUpdatePostTest()
        {
            try
            {
                var rankingController = new RankingController();

                var ranking = rankingController.GetRankingInfo("A000003", 101);

                Assert.AreEqual("A000003", ranking.StudentId);
                Assert.AreEqual(101, ranking.ScheduleId);
                Assert.AreEqual(1, ranking.Rank);

                rankingController.UpdateRanking(new Ranking
                {
                    StudentId = "A000003",
                    ScheduleId = 101,
                    Rank = 50
                });

                ranking = rankingController.GetRankingInfo("A000003", 101);

                Assert.AreEqual("A000003", ranking.StudentId);
                Assert.AreEqual(101, ranking.ScheduleId);
                Assert.AreEqual(50, ranking.Rank);

                rankingController.UpdateRanking(new Ranking
                {
                    StudentId = "A000003",
                    ScheduleId = 101,
                    Rank = 1
                });

                ranking = rankingController.GetRankingInfo("A000003", 101);

                Assert.AreEqual("A000003", ranking.StudentId);
                Assert.AreEqual(101, ranking.ScheduleId);
                Assert.AreEqual(1, ranking.Rank);
            }
            catch 
            { 
                throw; 
            }
        }
    }
}
