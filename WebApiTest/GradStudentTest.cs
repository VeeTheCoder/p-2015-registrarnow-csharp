// <copyright file="GradStudentTest.cs" company="CompanyName">
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
    public class GradStudentTest
    {
        /// <summary>
        /// Authorize test cases
        /// </summary>
        [TestMethod]
        public void GradStudentGetTest()
        {
            var gradStudentController = new GradStudentController();
            var gradStudent = gradStudentController.GetGradStudent("A000006");
            Assert.AreEqual("A000006", gradStudent.Sid);
        }
    }
}
