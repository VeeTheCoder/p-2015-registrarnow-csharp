// <copyright file="ForeignStudentTest.cs" company="CompanyName">
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
    public class ForeignStudentTest
    {
        /// <summary>
        /// Authorize test cases
        /// </summary>
        [TestMethod]
        public void ForeignStudentGetTest()
        {
            var foreignStudentController = new ForeignStudentController();
            var foreignStudent = foreignStudentController.GetForeignStudent("A000007");
            Assert.AreEqual("A000007", foreignStudent.Sid);
        }
    }
}
