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
    public class GradeDistributionServiceTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertGradeDistributionErrorTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IGradeDistributionRepository>();
            var gradeDistributionService = new GradeDistributionService(mockRepository.Object);

            //// Act
            gradeDistributionService.InsertGradeDistribution(null, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertGradeDistributionTest2()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IGradeDistributionRepository>();
            var gradeDistributionService = new GradeDistributionService(mockRepository.Object);
            var gradeDistribution = new GradeDistribution
            {
                Schedule_id = -1,
                Grade_Distribution = string.Empty,
                Title = string.Empty
            };

            //// Act
            gradeDistributionService.InsertGradeDistribution(gradeDistribution, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void GradeDistributionErrorTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IGradeDistributionRepository>();
            var gradeDistributionService = new GradeDistributionService(mockRepository.Object);

            //// Act
            var gradeDistributionlist = new List<GradeDistribution>();
            gradeDistributionlist.Add(new GradeDistribution { Schedule_id = 104, Grade_Distribution = "10,40,30,10,10", Title = "CSE" });
            gradeDistributionlist.Add(new GradeDistribution { Schedule_id = 103, Grade_Distribution = "10,10,30,20,30", Title = "CSE" });
            mockRepository.Setup(x => x.GetGradeDistribution(ref errors)).Returns(gradeDistributionlist);

            //// Act
            var testList = gradeDistributionService.GetGradeDistribution(ref errors);

            //// Assert
            Assert.AreEqual(gradeDistributionlist, testList);
        }
    }
}