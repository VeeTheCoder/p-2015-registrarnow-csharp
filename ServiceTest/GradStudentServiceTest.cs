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
    public class GradStudentServiceTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertGradStudentErrorTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IGradStudentRepository>();
            var gradStudentService = new GradStudentService(mockRepository.Object);

            //// Act
            gradStudentService.InsertGradStudent(null, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertGradStudentErrorTest2()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IGradStudentRepository>();
            var gradStudentService = new GradStudentService(mockRepository.Object);
            var gradStudent = new GradStudent { Sid = string.Empty };

            //// Act
            gradStudentService.InsertGradStudent(gradStudent, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GradStudentErrorTest()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IGradStudentRepository>();
            var gradStudentService = new GradStudentService(mockRepository.Object);

            //// Act
            gradStudentService.GetGradStudent(null, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteGradStudentErrorTest()
        {
            //// Arrange
            var errors = new List<string>();

            var mockRepository = new Mock<IGradStudentRepository>();
            var gradStudentService = new GradStudentService(mockRepository.Object);

            //// Act
            gradStudentService.DeleteGradStudent(null, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateGpaErrorTest()
        {
            //// Arrange
            var errors = new List<string>();

            var mockRepository = new Mock<IGradStudentRepository>();
            var gradStudentService = new GradStudentService(mockRepository.Object);

            //// Act
            gradStudentService.CalculateGpa(string.Empty, null, ref errors);

            //// Assert
            Assert.AreEqual(2, errors.Count);
        }

        [TestMethod]
        public void CalculateGpaNoEnrollmentTest()
        {
            //// Arrange
            var errors = new List<string>();

            var mockRepository = new Mock<IGradStudentRepository>();
            var gradStudentService = new GradStudentService(mockRepository.Object);
            mockRepository.Setup(x => x.GetEnrollments("testId")).Returns(new List<Enrollment>());

            //// Act
            var enrollments = gradStudentService.GetEnrollments("testId", ref errors);
            var gap = gradStudentService.CalculateGpa("testId", enrollments, ref errors);

            //// Assert
            Assert.AreEqual(0, errors.Count);
            Assert.AreEqual(0.0f, gap);
        }

        [TestMethod]
        public void CalculateGpaWithEnrollmentTest()
        {
            //// Arrange
            var errors = new List<string>();

            var mockRepository = new Mock<IGradStudentRepository>();
            var gradStudentService = new GradStudentService(mockRepository.Object);
            var enrollments = new List<Enrollment>();
            enrollments.Add(new Enrollment { Grade = "A", GradeValue = 4.3f, ScheduleId = 1, StudentId = "testId" });
            enrollments.Add(new Enrollment { Grade = "B", GradeValue = 3.3f, ScheduleId = 2, StudentId = "testId" });
            enrollments.Add(new Enrollment { Grade = "C+", GradeValue = 2.7f, ScheduleId = 3, StudentId = "testId" });

            mockRepository.Setup(x => x.GetEnrollments("testId")).Returns(enrollments);

            //// Act
            var gap = gradStudentService.CalculateGpa("testId", enrollments, ref errors);

            //// Assert
            Assert.AreEqual(0, errors.Count);
            Assert.AreEqual(true, gap > 3.2f && gap < 3.5f);
        }
    }
}
