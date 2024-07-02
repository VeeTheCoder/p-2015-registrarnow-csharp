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
    public class AcademicAdvisingServiceTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertAcademicAdvisingMessageErrorTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IAcademicAdvisingRepository>();
            var academicAdvisingService = new AcademicAdvisingService(mockRepository.Object);

            //// Act
            academicAdvisingService.InsertAdvisingMessage(null, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertAcademicAdvisingMessageErrorTest2()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IAcademicAdvisingRepository>();
            var academicAdvisingService = new AcademicAdvisingService(mockRepository.Object);
            var message = new AcademicAdvisingMessage { StudentId = string.Empty };

            //// Act
            academicAdvisingService.InsertAdvisingMessage(message, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertAcademicAdvisingMessageErrorTest3()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IAcademicAdvisingRepository>();
            var academicAdvisingService = new AcademicAdvisingService(mockRepository.Object);
            var message = new AcademicAdvisingMessage { MessageBody = string.Empty };

            //// Act
            academicAdvisingService.InsertAdvisingMessage(message, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void InsertAcademicAdvisingMessageErrorTest4()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IAcademicAdvisingRepository>();
            var academicAdvisingService = new AcademicAdvisingService(mockRepository.Object);
            var message = new AcademicAdvisingMessage { StudentId = "test", MessageBody = "test", MessageSubject = "test" };

            //// Act
            academicAdvisingService.InsertAdvisingMessage(message, ref errors);

            //// Assert
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateAcademicAdvisingMessageErrorTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IAcademicAdvisingRepository>();
            var academicAdvisingService = new AcademicAdvisingService(mockRepository.Object);

            //// Act
            academicAdvisingService.UpdateAdvisingMessage(null, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void DeleteAcademicAdvisingMessageErrorTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IAcademicAdvisingRepository>();
            var academicAdvisingService = new AcademicAdvisingService(mockRepository.Object);

            //// Act
            academicAdvisingService.DeleteAdvisingMessage(0, ref errors);

            //// Assert
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]     
        public void GetAdvisingMessageDetailErrorTest()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IAcademicAdvisingRepository>();
            var academicAdvisingService = new AcademicAdvisingService(mockRepository.Object);

            //// Act
            academicAdvisingService.GetAdvisingMessageDetail(0, ref errors);

            //// Assert
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetAdvisingMessagesErrorTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IAcademicAdvisingRepository>();
            var academicAdvisingService = new AcademicAdvisingService(mockRepository.Object);

            //// Act
            academicAdvisingService.GetAdvisingMessages(null, false, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }
    }
}
