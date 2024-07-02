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
    public class EnrollmentServiceTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertEnrollmentErrorTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IEnrollmentRepository>();
            var enrollmentService = new EnrollmentService(mockRepository.Object);

            //// Act
            enrollmentService.InsertEnrollment(null, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertEnrollmentErrorTest2()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IEnrollmentRepository>();
            var enrollmentService = new EnrollmentService(mockRepository.Object);
            var enrollment = new Enrollment 
                    { 
                        StudentId = string.Empty,
                        ScheduleId = -1,
                        Grade = "AB",
                        GradeValue = -1.0f
                    };

            //// Act
            enrollmentService.InsertEnrollment(enrollment, ref errors);

            //// Assert
            Assert.AreEqual(4, errors.Count);
        }

        [TestMethod]
        public void EnrollmentErrorTest()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IEnrollmentRepository>();
            var enrollmentService = new EnrollmentService(mockRepository.Object);

            //// Act
            enrollmentService.GetEnrollmentInfo(null, -1, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteEnrollmentErrorTest()
        {
            //// Arrange
            var errors = new List<string>();

            var mockRepository = new Mock<IEnrollmentRepository>();
            var enrollmentService = new EnrollmentService(mockRepository.Object);

            //// Act
            enrollmentService.DeleteEnrollment(null, -1, ref errors);

            //// Assert
            Assert.AreEqual(2, errors.Count);
        }
    }
}