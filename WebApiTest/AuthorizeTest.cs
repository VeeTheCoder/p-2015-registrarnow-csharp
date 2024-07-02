// <copyright file="AuthorizeTest.cs" company="CompanyName">
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
    public class AuthorizeTest
    {
        /// <summary>
        /// Authorize test cases
        /// </summary>
        [TestMethod]
        public void AuthorizeGetTest()
        {
            var authorizeController = new AuthorizeController();
            var login = authorizeController.Authenticate("admin@cs.ucsd.edu", "password");
            Assert.AreEqual("1", login.Id);
        }

        /// <summary>
        /// Authorize test cases
        /// </summary>
        [TestMethod]
        public void AuthorizePostTest()
        {
            var authorizeController = new AuthorizeController();
            var login = authorizeController.Authenticate(new Logon { UserName = "admin@cs.ucsd.edu", Password = "password" });
            Assert.AreEqual("1", login.Id);
        }
    }
}
