using NUnit.Framework;

using dotDashboard.Areas.Api.Controllers;
using dotDashboard.Areas.Api.Models;

namespace UnitTests.Api
{
    [TestFixture]
    public class Controllers
    {
        [Test]
        public void Should_test_get()
        {
            var commentsController = new CommentsController();
            var result = commentsController.CommentList(null, null);
        }

        [Test]
        public void Should_Add_Service_Event()
        {
            var commentsController = new CommentsController();
            var c = new Comment();
            c.AuthorName = "artur";
            c.Body = "boody";
            c.Subject = "sub";
            var result = commentsController.Comment(null, c, "POST");
        }
    }
}