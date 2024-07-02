// <copyright file="GradeDistributionTest.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
namespace WebApiTest
{
    using System;
    using System.Collections.Generic;

    using IRepository;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using POCO;
    using WebApi.Controllers;

    /// <summary>
    /// Authorize test cases
    /// </summary>
    [TestClass]
    public class GradeDistributionTest
    {
        /// <summary>
        /// Authorize test cases
        /// </summary>
        [TestMethod]
        public void GradeDistributionGetTest()
        {
            var gradeDistributionController = new GradeDistributionController();
            var gradeDistribution = gradeDistributionController.GetGradeDistribution();

            var errors = new List<string>();

            //// Act
            Assert.AreEqual(gradeDistribution[0].Schedule_id, 100);
            Assert.AreEqual(gradeDistribution[0].Grade_Distribution, "10,20,30,10,30");
            Assert.AreEqual(gradeDistribution[0].Title, "CSE");
            Assert.AreEqual(gradeDistribution[1].Schedule_id, 101);
            Assert.AreEqual(gradeDistribution[1].Grade_Distribution, "10,20,30,30,10");
            Assert.AreEqual(gradeDistribution[1].Title, "CSE");
        }
    }
}
