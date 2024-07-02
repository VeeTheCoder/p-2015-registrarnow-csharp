// <copyright file="FinalScheduleTest.cs" company="CompanyName">
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
    public class FinalScheduleTest
    {
        /// <summary>
        /// Authorize test cases
        /// </summary>
        [TestMethod]
        public void FinalScheduleGetTest()
        {
            var finalScheduleController = new FinalScheduleController();
            var finalSchedule = finalScheduleController.GetFinalSchedule();

            var errors = new List<string>();

            //// Act
            Assert.AreEqual(finalSchedule[0].Schedule_id, 100);
            Assert.AreEqual(finalSchedule[0].FinalLocation, "PCYNH");
            Assert.AreEqual(finalSchedule[0].FinalTime, "19:30:00");
            Assert.AreEqual(finalSchedule[0].Title, "CSE");
        }
    }
}
