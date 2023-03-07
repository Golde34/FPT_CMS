using Server.Entity;

namespace Server.Repository.@interface
{
    public interface ICommentRepo
    {
        Comment AddComment(Comment Comment);
        Comment UpdateComment(Comment Comment);
        Comment DeleteComment(Comment Comment);
        IEnumerable<Comment> GetComments();
        Comment GetCommentById(int? id);
    }
}
