// <copyright file="EnrollmentTest.cs" company="CompanyName">
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
    public class EnrollmentTest
    {
        /// <summary>
        /// Authorize test cases
        /// </summary>
        [TestMethod]
        public void EnrollmentGetTest()
        {
            var enrollmentController = new EnrollmentController();
            var enrollment = enrollmentController.GetEnrollmentInfo("A000001", 101);
            Assert.AreEqual("A000001", enrollment[0].StudentId);
            Assert.AreEqual(101, enrollment[0].ScheduleId);
            Assert.AreEqual("C", enrollment[0].Grade);
        }

        /// <summary>
        /// Authorize test cases
        /// </summary>
        [TestMethod]
        public void EnrollmentInsertDeletePostTest()
        {
            try
            {
                var enrollmentController = new EnrollmentController();

                enrollmentController.DeleteEnrollment("A000005", 105);

                var enrollment = enrollmentController.GetEnrollmentInfo("A000005", 105);
                Assert.AreEqual(null, enrollment);

                enrollmentController.InsertEnrollment(new Enrollment
                {
                    StudentId = "A000005",
                    ScheduleId = 105,
                    Grade = "D+"
                });

                enrollment = enrollmentController.GetEnrollmentInfo("A000005", 105);
                Assert.AreEqual("A000005", enrollment[0].StudentId);
                Assert.AreEqual(105, enrollment[0].ScheduleId);
                Assert.AreEqual("D+", enrollment[0].Grade);

                enrollmentController.DeleteEnrollment("A000005", 105);

                enrollment = enrollmentController.GetEnrollmentInfo("A000005", 105);
                Assert.AreEqual(null, enrollment);
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
        public void EnrollmentUpdatePostTest()
        {
            try
            {
                var enrollmentController = new EnrollmentController();

                var enrollment = enrollmentController.GetEnrollmentInfo("A000002", 102);

                Assert.AreEqual("A000002", enrollment[0].StudentId);
                Assert.AreEqual(102, enrollment[0].ScheduleId);
                Assert.AreEqual("A", enrollment[0].Grade);

                enrollmentController.UpdateEnrollment(new Enrollment
                {
                    StudentId = "A000002",
                    ScheduleId = 102,
                    Grade = "B-"
                });

                enrollment = enrollmentController.GetEnrollmentInfo("A000002", 102);

                Assert.AreEqual("A000002", enrollment[0].StudentId);
                Assert.AreEqual(102, enrollment[0].ScheduleId);
                Assert.AreEqual("B-", enrollment[0].Grade);

                enrollmentController.UpdateEnrollment(new Enrollment
                {
                    StudentId = "A000002",
                    ScheduleId = 102,
                    Grade = "A"
                });

                enrollment = enrollmentController.GetEnrollmentInfo("A000002", 102);

                Assert.AreEqual("A000002", enrollment[0].StudentId);
                Assert.AreEqual(102, enrollment[0].ScheduleId);
                Assert.AreEqual("A", enrollment[0].Grade);
            }
            catch 
            { 
                throw; 
            }
        }
    }
}
