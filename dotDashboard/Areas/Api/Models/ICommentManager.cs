using System.Collections.Generic;

namespace dotDashboard.Areas.Api.Models
{
    public interface ICommentManager
    {
        Comment Create(Comment item);
        List<int> CreateComments(List<Comment> items);
        Comment Update(Comment item);
        Comment GetById(int id);
        List<Comment> GetComments(int? page, int? count);
        bool Delete(int id);
    }
}