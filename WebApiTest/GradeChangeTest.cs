// <copyright file="GradeChangeTest.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
namespace WebApiTest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using POCO;

    using WebApi.Controllers;

    /// <summary>
    /// Authorize test cases
    /// </summary>
    [TestClass]
    public class GradeChangeTest
    {
        /// <summary>
        /// Authorize test cases
        /// </summary>
        [TestMethod]
        public void GradeChangeGetTest()
        {
            var gradeChangeController = new GradeChangeController();
            var message = gradeChangeController.GetGradeChangeMessageDetail(1);
            Assert.AreEqual(100, message.ScheduleId);
            Assert.AreEqual("A000001", message.StudentId);
            Assert.AreEqual("Hello", message.MessageBody);
            Assert.AreEqual(1, message.InstructorId);
        }
    }
}
