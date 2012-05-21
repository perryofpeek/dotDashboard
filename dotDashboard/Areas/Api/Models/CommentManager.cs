using System.Collections.Generic;

namespace dotDashboard.Areas.Api.Models
{
    // NOTE: All methods are returning mock data. These should be refactored to work with a data source.
    public class CommentManager : ICommentManager
    {
        public Comment Create(Comment item)
        {
            item.Id = 1;
            return item;
        }

        public List<int> CreateComments(List<Comment> items)
        {
            return new List<int> { 1, 2, 3 };
        }

        public Comment Update(Comment item) { return item; }

        public Comment GetById(int id)
        {
            return new Comment
            {
                Id = id,
                Subject = "Loaded Subject",
                Body = "Loaded Body",
                AuthorName = "Loaded Author"
            };
        }

        public List<Comment> GetComments(int? page, int? count)
        {
            var comment1 = new Comment
            {
                Id = 1,
                Subject = "First Subject",
                Body = "First Body",
                AuthorName = "First Author"
            };
            var comment2 = new Comment
            {
                Id = 2,
                Subject = "Second Subject",
                Body = "Second Body",
                AuthorName = "Second Author"
            };
            var items = new List<Comment> { comment1, comment2 };
            return items;
        }

        public bool Delete(int id) { return true; }
    }
}