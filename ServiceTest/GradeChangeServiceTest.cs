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
    public class GradeChangeServiceTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertGradeChangeMessageErrorTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IGradeChangeRepository>();
            var gradeChangeService = new GradeChangeService(mockRepository.Object);

            //// Act
            gradeChangeService.InsertGradeChangeMessage(null, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertGradeChangeMessageErrorTest2()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IGradeChangeRepository>();
            var gradeChangeService = new GradeChangeService(mockRepository.Object);
            var message = new GradeChangeMessage { StudentId = string.Empty }; 

            //// Act
            gradeChangeService.InsertGradeChangeMessage(message, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertGradeChangeMessageErrorTest3()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IGradeChangeRepository>();
            var gradeChangeService = new GradeChangeService(mockRepository.Object);
            var message = new GradeChangeMessage { MessageBody = string.Empty };

            //// Act
            gradeChangeService.InsertGradeChangeMessage(message, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void InsertGradeChangeMessageErrorTest5()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IGradeChangeRepository>();
            var gradeChangeService = new GradeChangeService(mockRepository.Object);
            var message = new GradeChangeMessage { StudentId = "test", MessageBody = "test" };

            //// Act
            gradeChangeService.InsertGradeChangeMessage(message, ref errors);

            //// Assert
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateGradeChangeMessageErrorTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IGradeChangeRepository>();
            var gradeChangeService = new GradeChangeService(mockRepository.Object);

            //// Act
            gradeChangeService.UpdateGradeChangeMessage(null, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void UpdateGradeChangeMessageErrorTest2()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IGradeChangeRepository>();
            var gradeChangeService = new GradeChangeService(mockRepository.Object);
            var message = new GradeChangeMessage { StudentId = "test", MessageBody = "test" };

            //// Act
            gradeChangeService.UpdateGradeChangeMessage(message, ref errors);

            //// Assert
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void DeleteGradeChangeMessageErrorTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IGradeChangeRepository>();
            var gradeChangeService = new GradeChangeService(mockRepository.Object);

            //// Act
            gradeChangeService.DeleteGradeChangeMessage(1, ref errors);

            //// Assert
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void GetGradeChangeDetailErrorTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IGradeChangeRepository>();
            var gradeChangeService = new GradeChangeService(mockRepository.Object);

            //// Act
            gradeChangeService.GetGradeChangeMessageDetail(1, ref errors);

            //// Assert
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void GetGradeChangeMessagesErrorTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IGradeChangeRepository>();
            var gradeChangeService = new GradeChangeService(mockRepository.Object);

            //// Act
            gradeChangeService.GetGradeChangeMessages("A000001", true, ref errors);

            //// Assert
            Assert.AreEqual(0, errors.Count);
        }
    }
}
