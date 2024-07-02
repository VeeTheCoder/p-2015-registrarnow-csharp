// <copyright file="AcademicAdvisingTest.cs" company="CompanyName">
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
    public class AcademicAdvisingTest
    {
        /// <summary>
        /// Authorize test cases
        /// </summary>
        [TestMethod]
        public void AcademicAdvisingGetTest()
        {
              var academicAdvisingController = new AcademicAdvisingController();
              var message = academicAdvisingController.GetAdvisingMessageDetail(1);
              Assert.AreEqual("A000001", message.StudentId);
              Assert.IsTrue(message.SendToInstructor);
              Assert.AreEqual("Grade mishap?", message.MessageSubject);
              Assert.AreEqual("John Doe", message.StudentName);
        }
    }
}
