namespace ServiceTest
{
    using System;
    using System.Collections.Generic;

    using IRepository;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using POCO;
    using Service;

    [TestClass]
    public class RankingServiceTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertRankingErrorTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IRankingRepository>();
            var enrollmentService = new RankingService(mockRepository.Object);

            //// Act
            enrollmentService.InsertRanking(null, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertRankingErrorTest2()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IRankingRepository>();
            var rankingService = new RankingService(mockRepository.Object);
            var ranking = new Ranking
            {
                StudentId = string.Empty,
                ScheduleId = -1,
                Rank = -1
            };

            //// Act
            rankingService.InsertRanking(ranking, ref errors);

            //// Assert
            Assert.AreEqual(3, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RankingErrorTest()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IRankingRepository>();
            var rankingService = new RankingService(mockRepository.Object);

            //// Act
            rankingService.GetRankingInfo(null, -1, ref errors);

            //// Assert
            Assert.AreEqual(2, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteRankingErrorTest()
        {
            //// Arrange
            var errors = new List<string>();

            var mockRepository = new Mock<IRankingRepository>();
            var rankingService = new RankingService(mockRepository.Object);

            //// Act
            rankingService.DeleteRanking(null, -1, ref errors);

            //// Assert
            Assert.AreEqual(2, errors.Count);
        }
    }
}