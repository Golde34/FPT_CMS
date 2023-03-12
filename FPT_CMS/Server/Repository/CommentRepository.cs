using Server.DAO;
using Server.Entity;
using Server.Repository.@interface;

namespace Server.Repository
{
    public class CommentRepository : ICommentRepo
    {
        Comment ICommentRepo.AddComment(Comment Comment) => CommentManagement.Instance.AddComment(Comment);
        Comment ICommentRepo.UpdateComment(Comment Comment) => CommentManagement.Instance.UpdateComment(Comment);
        Comment ICommentRepo.DeleteComment(Comment Comment) => CommentManagement.Instance.DeleteComment(Comment);
        IEnumerable<Comment> ICommentRepo.GetComments() => CommentManagement.Instance.GetComments();
        Comment ICommentRepo.GetCommentById(int? id) => CommentManagement.Instance.GetCommentById(id);
        IEnumerable<Comment> ICommentRepo.GetCommentsByNotification(int notificationId) => CommentManagement.Instance.GetCommentsByNotification(notificationId);
    }
}
