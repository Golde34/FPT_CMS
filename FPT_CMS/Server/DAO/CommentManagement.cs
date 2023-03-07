using Microsoft.EntityFrameworkCore;
using Server.Entity;

namespace Server.DAO
{
    public class CommentManagement
    {
        private static CommentManagement instance;
        private static readonly object instancelock = new object();

        public CommentManagement() { }

        public static CommentManagement Instance
        {
            get
            {
                lock (instancelock)
                {
                    if (instance == null) instance = new CommentManagement();
                }
                return instance;
            }
        }

        public Comment AddComment(Comment Comment)
        {
            try
            {
                Comment _Comment = GetCommentById(Comment.Id);
                if (_Comment == null)
                {
                    var context = new AppDBContext();
                    context.Comments.Add(Comment);
                    context.SaveChanges();
                    return Comment;
                }
                else
                {
                    throw new Exception("The Comment's username has already bean taken.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Comment UpdateComment(Comment Comment)
        {
            try
            {
                Comment _Comment = GetCommentById(Comment.Id);
                if (_Comment != null)
                {
                    var context = new AppDBContext();
                    context.Entry<Comment>(Comment).State = EntityState.Modified;
                    context.SaveChanges();
                    return Comment;
                }
                else
                {
                    throw new Exception("The Comment is not exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Comment DeleteComment(Comment Comment)
        {
            try
            {
                Comment _Comment = GetCommentById(Comment.Id);
                if (_Comment != null)
                {
                    var context = new AppDBContext();
                    context.Comments.Remove(_Comment);
                    context.SaveChanges();
                    return _Comment;
                }
                else
                {
                    throw new Exception("The Comment is not exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<Comment> GetComments()
        {
            List<Comment> Comments;
            try
            {
                var context = new AppDBContext();
                Comments = context.Comments.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Comments;
        }

        public Comment GetCommentById(int? id)
        {
            Comment Comment;
            try
            {
                var _dbContext = new AppDBContext();
                Comment = _dbContext.Comments
                    .FirstOrDefault(a => a.Id == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Comment;
        }
    }
}
