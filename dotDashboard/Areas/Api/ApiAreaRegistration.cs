using System.Web.Mvc;

namespace dotDashboard.Areas.Api
{
    public class ApiAreaRegistration : AreaRegistration
    {
        public override string AreaName { get { return "Api"; } }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("SingleComment", "Api/Comments/Comment/{id}", new
                {
                    controller = "Comments",
                    action = "Comment",
                    id = UrlParameter.Optional
                }
            );
            context.MapRoute("ListComments", "Api/Comments/{page}/{count}", new
                {
                    controller = "Comments",
                    action = "CommentList",
                    page = UrlParameter.Optional,
                    count = UrlParameter.Optional
                }
            );
            context.MapRoute("ListCommentsAll", "Api/Comments", new
                {
                    controller = "Comments",
                    action = "CommentList",
                    page = UrlParameter.Optional,
                    count = UrlParameter.Optional
                }
            );

            context.MapRoute("Api_default", "Api/{controller}/{action}/{id}", new { action = "Index", id = UrlParameter.Optional });
        }
    }
}