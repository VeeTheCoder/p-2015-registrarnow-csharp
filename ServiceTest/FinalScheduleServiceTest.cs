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
    public class FinalScheduleServiceTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertFinalScheduleErrorTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IFinalScheduleRepository>();
            var finalScheduleService = new FinalScheduleService(mockRepository.Object);

            //// Act
            finalScheduleService.InsertFinalSchedule(null, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertFinalScheduleTest2()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IFinalScheduleRepository>();
            var finalScheduleService = new FinalScheduleService(mockRepository.Object);
            var finalSchedule = new FinalSchedule
            {
                Schedule_id = -1,
                FinalLocation = string.Empty,
                FinalTime = string.Empty,
                Title = string.Empty
            };

            //// Act
            finalScheduleService.InsertFinalSchedule(finalSchedule, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void FinalScheduleErrorTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IFinalScheduleRepository>();
            var finalScheduleService = new FinalScheduleService(mockRepository.Object);

            //// Act
            var finalSchedulelist = new List<FinalSchedule>();
            finalSchedulelist.Add(new FinalSchedule { Schedule_id = 104, FinalLocation = "EBU1", FinalTime = "10:00:00", Title = "CSE" });
            finalSchedulelist.Add(new FinalSchedule { Schedule_id = 103, FinalLocation = "PCYNC", FinalTime = "10:20:00", Title = "CSE" });
            mockRepository.Setup(x => x.GetFinalSchedule(ref errors)).Returns(finalSchedulelist);

            //// Act
            var testList = finalScheduleService.GetFinalSchedule(ref errors);

            //// Assert
            Assert.AreEqual(finalSchedulelist, testList);
        }
    }
}