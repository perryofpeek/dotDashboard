using System.Web.Mvc;

using NUnit.Framework;

using dotDash.Domain;

using dotDashboard.Controllers;

namespace UnitTests.Website.Views
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class TestView
    {
        [Test]
        public void Should_Get_View()
        {
            var controller = new RRApiController();
            var result = controller.Details("name");
            Assert.AreEqual("Details", result.ViewName);
        }

        [Test]
        public void Should_Get_View_Details()
        {
            var controller = new RRApiController();
            var result = controller.Details("name");
            Assert.AreEqual("Details", result.ViewName);
            var serviceEvent = (ServiceEvent)result.ViewData.Model;
            Assert.That(serviceEvent.State, Is.EqualTo("up"));
        }

        [Test]
        public void Should_Get_View_Index()
        {
            var controller = new RRApiController();
            ViewResult result = controller.Index("name");            
        }
    }
}